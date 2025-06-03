using AutoMapper;
using Cafe.Application.DTOs.ProductIngredients;
using Cafe.Application.DTOs.Products;
using Cafe.Application.Interfaces.Services.Contracts;
using Cafe.Application.Repositories;
using Cafe.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Cafe.Core.Utilities.Results;
using Cafe.Application.Validators.Products;
using Cafe.Core.Aspects.Validation;

namespace Cafe.Application.Services.Managers
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IProductIngredientDal _productIngredientDal;
        private readonly IIngredientDal _ingredientDal;
        private readonly IProductionHistoryDal _productionHistoryDal;
        IFileService _fileService;

        private readonly IMapper _mapper;

        public ProductManager(IProductDal productDal, IMapper mapper, IProductIngredientDal productIngredientDal, IIngredientDal ingredientDal,
            IProductionHistoryDal productionHistoryDal, IFileService fileService)
        {
            _productDal = productDal;
            _mapper = mapper;
            _productIngredientDal = productIngredientDal;
            _ingredientDal = ingredientDal;
            _productionHistoryDal = productionHistoryDal;
            _fileService = fileService;
        }
        // [TransactionScopeAspect]
        [ValidationAspect(typeof(ProductCreateDtoValidator))]
        public async Task<IResult> Add(ProductCreateDto productCreateDto)
        {
            // 1. Malzeme stok kontrolü
            foreach (var item in productCreateDto.ProductIngredients!)
            {
                var ingredient = await _ingredientDal.GetAsync(i => i.Id == item.IngredientId);
                if (ingredient == null)
                    return new ErrorResult($"Malzeme bulunamadı. ID: {item.IngredientId}");

                var required = item.QuantityRequired * productCreateDto.Stock;
                if (ingredient.Stock < required)
                    return new ErrorResult($"Yetersiz stok: {ingredient.Name}. Gerekli: {required}, Mevcut: {ingredient.Stock}");
            }

            // 2. Görsel yükle
            var fileName = await _fileService.UploadImageAsync(productCreateDto.Image, "images/products");

            // 3. Ürün oluştur
            var product = new Product
            {
                Name = productCreateDto.Name,
                Description = productCreateDto.Description,
                Price = productCreateDto.Price,
                Stock = productCreateDto.Stock,
                CreatedAt = DateTime.UtcNow,
                ImageFileName = fileName,
            };

            await _productDal.AddAsync(product);

            // 4. Üretim geçmişi kaydı
            await _productionHistoryDal.AddAsync(new ProductionHistory
            {
                ProductId = product.Id,
                QuantityProduced = product.Stock,
                ProducedAt = DateTime.UtcNow
            });

            // 5. Malzeme ilişkisi ve stok düşümü
            foreach (var item in productCreateDto.ProductIngredients!)
            {
                var ingredient = await _ingredientDal.GetAsync(i => i.Id == item.IngredientId);
                if (ingredient == null)
                    return new ErrorResult($"Stok güncellemede malzeme eksik: {item.IngredientId}");

                // Malzeme ilişkisi ekle
                await _productIngredientDal.AddAsync(new ProductIngredient
                {
                    ProductId = product.Id,
                    IngredientId = item.IngredientId,
                    QuantityRequired = item.QuantityRequired
                });

                // Stok güncelle
                ingredient.Stock -= item.QuantityRequired * product.Stock;
                await _ingredientDal.UpdateAsync(ingredient);

                // Kritik stok uyarısı
                if (ingredient.Stock <= ingredient.MinStockThreshold)
                {
                    Console.WriteLine($"⚠️ Kritik stok: {ingredient.Name} | Mevcut: {ingredient.Stock}, Eşik: {ingredient.MinStockThreshold}");
                }
            }

            return new SuccessResult("Ürün ve malzemeleri başarıyla eklendi.");
        }

        public async Task<IDataResult<List<ProductGetDto>>> SearchProductsAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new ErrorDataResult<List<ProductGetDto>>("Arama terimi boş olamaz.");

            var allProducts = await _productDal.GetProductDetailsAsync();
            var matches = allProducts
                .Where(p => p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                            (!string.IsNullOrWhiteSpace(p.Description) && p.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            if (!matches.Any())
                return new SuccessDataResult<List<ProductGetDto>>(new List<ProductGetDto>(), "Eşleşen ürün bulunamadı.");

            return new SuccessDataResult<List<ProductGetDto>>(matches);
        }

        public async Task<IDataResult<List<ProductIngredientDto>>> GetProductRecipeAsync(int productId)
        {
            var ingredients = await _productIngredientDal.GetAllWithIngredientAsync(productId); // Özel metot: Ingredient join'li çekiyor varsayalım
            if (ingredients == null || !ingredients.Any())
                return new SuccessDataResult<List<ProductIngredientDto>>(new List<ProductIngredientDto>(), "Reçete bulunamadı.");

            var dtoList = _mapper.Map<List<ProductIngredientDto>>(ingredients);
            return new SuccessDataResult<List<ProductIngredientDto>>(dtoList);
        }

        public async Task<IResult> Delete(int productId)
        {
            var product = await _productDal.GetAsync(p => p.Id == productId);
            if (product == null)
                return new ErrorResult("Ürün bulunamadı.");

            // Malzeme ilişkilerini sil
            var productIngredients = await _productIngredientDal.GetAllAsync(pi => pi.ProductId == productId);
            foreach (var pi in productIngredients)
            {
                await _productIngredientDal.DeleteAsync(pi);
            }

            // Ürünü sil (EF Core artık ProductId'yi ProductionHistory'de null yapacak)
            await _productDal.DeleteAsync(product);

            return new SuccessResult("Ürün ve malzeme ilişkileri silindi.");
        }



        public async Task<IDataResult<List<ProductGetDto>>> GetAllAsync()
        {
            var result = await _productDal.GetProductDetailsAsync(); // Bu zaten senkron bir metod

            return new SuccessDataResult<List<ProductGetDto>>(result);

            // return new SuccessDataResult<List<Product>>(await _productDal.GetAllAsync());
        }

        public async Task<IDataResult<ProductGetDto?>> GetById(int id)
        {
            var result = await _productDal.GetProductDetailsAsync();
            var pruduct= result.FirstOrDefault(p=>p.Id==id);
            if(pruduct == null)
            {
                return new ErrorDataResult<ProductGetDto?>("Ürün bulunamadı");
            }
          
            return new SuccessDataResult<ProductGetDto?>(pruduct);
        }

        // [TransactionScopeAspect]
        [ValidationAspect(typeof(ProductUpdateDtoValidator))]
        public async Task<IResult> Update(ProductUpdateDto productUpdateDto)
        {
            var product = await _productDal.GetAsync(p => p.Id == productUpdateDto.Id);
            if (product == null)
                return new ErrorResult("Ürün bulunamadı.");

            // 🔄 Görsel güncellemesi varsa
            if (productUpdateDto.Image != null)
            {
                await _fileService.DeleteImageAsync("images/products", product.ImageFileName!);
                var newImageName = await _fileService.UploadImageAsync(productUpdateDto.Image, "images/products");
                product.ImageFileName = newImageName;
            }

            // 📦 Temel bilgileri güncelle
            product.Name = productUpdateDto.Name;
            product.Description = productUpdateDto.Description;
            product.Price = productUpdateDto.Price;

            var stockDifference = productUpdateDto.Stock - product.Stock;
            bool isStockIncreased = stockDifference > 0;

            // 🔒 Stoğu artıracaksan ve reçete gönderildiyse, engelle!
            if (product.Stock > 0 && productUpdateDto.ProductIngredients != null)
            {
                return new ErrorResult("Stok > 0 iken ürünün reçetesi (malzemeleri) değiştirilemez.");
            }

            // 📝 Üretim geçmişi kaydı gerekiyorsa
            if (isStockIncreased)
            {
                var history = new ProductionHistory
                {
                    ProductId = product.Id,
                    QuantityProduced = stockDifference,
                    ProducedAt = DateTime.UtcNow
                };
                await _productionHistoryDal.AddAsync(history);
            }

            product.Stock = productUpdateDto.Stock;
            await _productDal.UpdateAsync(product);

            // 🔁 Reçete sadece yeni stok 0 ise güncellenebilir
            if (productUpdateDto.Stock == 0 && productUpdateDto.ProductIngredients != null)
            {
                // eski reçeteyi temizle
                var existingRelations = await _productIngredientDal.GetAllAsync(pi => pi.ProductId == product.Id);
                foreach (var relation in existingRelations)
                {
                    await _productIngredientDal.DeleteAsync(relation);
                }

                // yeni reçeteyi ekle
                foreach (var item in productUpdateDto.ProductIngredients)
                {
                    await _productIngredientDal.AddAsync(new ProductIngredient
                    {
                        ProductId = product.Id,
                        IngredientId = item.IngredientId,
                        QuantityRequired = item.QuantityRequired
                    });
                }
            }

            // 🔻 Malzeme stoğundan düşme işlemi
            if (isStockIncreased && productUpdateDto.ProductIngredients != null)
            {
                foreach (var item in productUpdateDto.ProductIngredients)
                {
                    var ingredient = await _ingredientDal.GetAsync(i => i.Id == item.IngredientId);
                    if (ingredient == null)
                        return new ErrorResult($"Malzeme bulunamadı (ID: {item.IngredientId})");

                    ingredient.Stock -= item.QuantityRequired * stockDifference;
                    await _ingredientDal.UpdateAsync(ingredient);

                    if (ingredient.Stock <= ingredient.MinStockThreshold)
                    {
                        Console.WriteLine($"⚠️ Kritik stok: {ingredient.Name} | Mevcut: {ingredient.Stock}, Eşik: {ingredient.MinStockThreshold}");
                    }
                }
            }

            return new SuccessResult("Ürün başarıyla güncellendi.");
        }



        public async Task<IDataResult<List<ProductProductionReportDto>>> GetMostProducedProductsAsync()
        {
            var products = await _productDal.GetAllAsync();

            var result = products
                .Select(p => new ProductProductionReportDto
                {
                    ProductName = p.Name,
                    TotalProduced = p.Stock
                })
                .OrderByDescending(p => p.TotalProduced)
                .ToList();

            return new SuccessDataResult<List<ProductProductionReportDto>>(result);
        }
        public async Task<IDataResult<List<ProductProductionHistoryDto>>> GetProductionHistoryReportAsync()
        {
            var records = await _productionHistoryDal.GetAllWithProductAsync();

            var report = records
                .GroupBy(p => p.ProductId) // Her ürün/silinmiş ürün ID'ye göre gruplanır
                .Select(g =>
                {
                    var firstRecord = g.First();
                    var product = firstRecord.Product;

                    return new ProductProductionHistoryDto
                    {
                        ProductName = product?.Name ?? "Silinmiş Ürün",
                        Description = product?.Description ?? "Bu ürün sistemden silinmiştir.",
                        TotalProduced = g.Sum(x => x.QuantityProduced),
                        LastProducedAt = g.Max(x => x.ProducedAt)
                    };
                })
                .OrderByDescending(x => x.TotalProduced)
                .ToList();

            return new SuccessDataResult<List<ProductProductionHistoryDto>>(report);
        }

        public async Task<IResult> ProduceProduct(int productId, int quantity)
        {
            var product = await _productDal.GetAsync(p => p.Id == productId);
            if (product == null)
                return new ErrorResult("Ürün bulunamadı.");

            // 1. Ingredient stok kontrolü
            var ingredients = await _productIngredientDal.GetAllAsync(pi => pi.ProductId == productId);
            foreach (var item in ingredients)
            {
                var ingredient = await _ingredientDal.GetAsync(i => i.Id == item.IngredientId);
                if (ingredient == null)
                    return new ErrorResult($"Malzeme bulunamadı (ID: {item.IngredientId})");

                var required = item.QuantityRequired * quantity;
                if (ingredient.Stock < required)
                    return new ErrorResult($"Yetersiz stok: {ingredient.Name}. Gerekli: {required}, Mevcut: {ingredient.Stock}");
            }

            // 2. Ingredient stoklarını düş
            foreach (var item in ingredients)
            {
                var ingredient = await _ingredientDal.GetAsync(i => i.Id == item.IngredientId)!;
                ingredient.Stock -= item.QuantityRequired * quantity;
                await _ingredientDal.UpdateAsync(ingredient);

                if (ingredient.Stock <= ingredient.MinStockThreshold)
                {
                    Console.WriteLine($"⚠️ Kritik stok: {ingredient.Name} | Mevcut: {ingredient.Stock}, Eşik: {ingredient.MinStockThreshold}");
                }
            }

            // 3. Ürün stoğunu artır
            product.Stock += quantity;
            await _productDal.UpdateAsync(product);

            // 4. Üretim geçmişi kaydı
            await _productionHistoryDal.AddAsync(new ProductionHistory
            {
                ProductId = product.Id,
                QuantityProduced = quantity,
                ProducedAt = DateTime.UtcNow
            });

            return new SuccessResult($"✅ {quantity} adet ürün başarıyla üretildi. Yeni stok: {product.Stock}");
        }

        private async Task<IResult> CheckIngredientStocksAsync(List<ProductIngredientCreateDto> ingredients, int stockAmount)
        {
            foreach (var item in ingredients)
            {
                var ingredient = await _ingredientDal.GetAsync(i => i.Id == item.IngredientId);
                if (ingredient == null)
                    return new ErrorResult($"Malzeme bulunamadı: ID {item.IngredientId}");

                var required = item.QuantityRequired * stockAmount;
                if (ingredient.Stock < required)
                    return new ErrorResult($"Yetersiz stok: {ingredient.Name}. Gerekli: {required}, Mevcut: {ingredient.Stock}");
            }
            return new SuccessResult();
        }
        private async Task<IDataResult<string?>> UploadImageAsync(IFormFile? image)
        {
            var fileName = await _fileService.UploadImageAsync(image, "images/products");
            return new SuccessDataResult<string?>(fileName);
        }
        private Product CreateProductEntity(ProductCreateDto dto, string? fileName)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CreatedAt = DateTime.UtcNow,
                ImageFileName = fileName
            };
        }
        private async Task LogProductionAsync(Product product)
        {
            var history = new ProductionHistory
            {
                ProductId = product.Id,
                QuantityProduced = product.Stock,
                ProducedAt = DateTime.UtcNow
            };
            await _productionHistoryDal.AddAsync(history);
        }
        private async Task<IResult> SaveIngredientRelationsAndUpdateStocksAsync(List<ProductIngredientCreateDto> ingredients, int stockAmount, int productId)
        {
            foreach (var item in ingredients)
            {
                await _productIngredientDal.AddAsync(new ProductIngredient
                {
                    ProductId = productId,
                    IngredientId = item.IngredientId,
                    QuantityRequired = item.QuantityRequired
                });

                var ingredient = await _ingredientDal.GetAsync(i => i.Id == item.IngredientId);
                if (ingredient == null)
                    return new ErrorResult($"Malzeme bulunamadı (stok güncelleme): ID {item.IngredientId}");

                ingredient.Stock -= item.QuantityRequired * stockAmount;
                await _ingredientDal.UpdateAsync(ingredient);

                if (ingredient.Stock <= ingredient.MinStockThreshold)
                {
                    Console.WriteLine($"⚠️ Kritik stok: {ingredient.Name} | Mevcut: {ingredient.Stock}, Eşik: {ingredient.MinStockThreshold}");
                }
            }
            return new SuccessResult();
        }
    }

}
