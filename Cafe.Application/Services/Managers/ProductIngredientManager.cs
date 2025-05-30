using AutoMapper;
using Cafe.Application.DTOs.Ingredients;
using Cafe.Application.DTOs.ProductIngredients;
using Cafe.Application.DTOs.Products;
using Cafe.Application.DTOs.Users;
using Cafe.Application.Interfaces.Services.Contracts;
using Cafe.Application.Repositories;
using Cafe.Application.Utilities.Results;
using Cafe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Services.Managers
{
    public class ProductIngredientManager : IProductIngredientService
    {
        private readonly IProductIngredientDal _productIngredientDal;
        private readonly IMapper _mapper;

        public ProductIngredientManager(IProductIngredientDal productIngredientDal, IMapper mapper)
        {
            _productIngredientDal = productIngredientDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(ProductIngredientCreateDto productIngredientCreateDto)
        {
            var newProIngrediton = _mapper.Map<ProductIngredient>(productIngredientCreateDto);
            await _productIngredientDal.AddAsync(newProIngrediton);
            return new SuccessResult();
            /*  var entity = new ProductIngredient
              {
                  ProductId = dto.ProductId,
                  IngredientId = dto.IngredientId,
                  QuantityRequired = dto.QuantityRequired
              };

              await _productIngredientDal.AddAsync(entity);
              return new SuccessResult("Reçete bilgisi eklendi.");*/
        }

        public async Task<IResult> DeleteAsync(int productId, int ingredientId)
        {
            var existing = await _productIngredientDal.GetAsync(pi => pi.ProductId == productId && pi.IngredientId == ingredientId);
            if (existing == null)
                return new ErrorResult("Silinecek malzeme bulunamadı.");

            await _productIngredientDal.DeleteAsync(existing);
            return new SuccessResult("Malzeme reçeteden silindi.");
        }
        public async Task<IResult> UpdateAsync(ProductIngredientUpdateDto dto)
        {
            var existing = await _productIngredientDal.GetAsync(pi => pi.ProductId == dto.ProductId && pi.IngredientId == dto.IngredientId);
            if (existing == null)
                return new ErrorResult("Malzeme bulunamadı.");

            existing.QuantityRequired = dto.QuantityRequired;
            await _productIngredientDal.UpdateAsync(existing);
            return new SuccessResult("Malzeme miktarı güncellendi.");
        }
        public async Task<IDataResult<List<GroupedProductIngredientsDto>>> GetAllAsync()
        {
            var list = await _productIngredientDal.GetAllWithProductAndIngredientAsync(); // bu metodu kullanıyoruz

            var grouped = list
                .GroupBy(x => x.ProductId)
                .Select(group => new GroupedProductIngredientsDto
                {
                    ProductId = group.Key,
                    ProductName = group.First().Product.Name,
                    Ingredients = _mapper.Map<List<ProductIngredientDto>>(group.ToList())
                })
                .ToList();

            return new SuccessDataResult<List<GroupedProductIngredientsDto>>(grouped);
        }
        public async Task<IDataResult<ProductIngredientGetDto>> GetById(int id)
        {
            var result = await _productIngredientDal.GetByIdWithDetailsAsync(id);
            if (result == null)
                return new ErrorDataResult<ProductIngredientGetDto>("Veri bulunamadı.");

            return new SuccessDataResult<ProductIngredientGetDto>(result);
        }
        public async Task<IDataResult<List<IngredientUsageReportDto>>> GetMostUsedIngredientsAsync()
        {
            var productIngredients = await _productIngredientDal.GetAllWithProductAndIngredientAsync();

            var result = productIngredients
                .GroupBy(pi => pi.Ingredient.Name)
                .Select(g => new IngredientUsageReportDto
                {
                    IngredientName = g.Key,
                    TotalUsedAmount = g.Sum(pi => pi.QuantityRequired * pi.Product.Stock)
                })
                .OrderByDescending(x => x.TotalUsedAmount)
                .ToList();

            return new SuccessDataResult<List<IngredientUsageReportDto>>(result);
        }
        public async Task<IDataResult<List<IngredientUsageReportDto>>> GetMostUsedIngredientsAsync(UsageReportFilterDto filter)
        {
            var productIngredients = await _productIngredientDal.GetAllWithProductAndIngredientAsync();

            if (filter.StartDate.HasValue)
                productIngredients = productIngredients
                    .Where(pi => pi.Product.CreatedAt >= filter.StartDate.Value)
                    .ToList();

            if (filter.EndDate.HasValue)
                productIngredients = productIngredients
                    .Where(pi => pi.Product.CreatedAt <= filter.EndDate.Value)
                    .ToList();

            var result = productIngredients
                .GroupBy(pi => pi.Ingredient.Name)
                .Select(g => new IngredientUsageReportDto
                {
                    IngredientName = g.Key,
                    TotalUsedAmount = g.Sum(pi => pi.QuantityRequired * pi.Product.Stock)
                })
                .OrderByDescending(x => x.TotalUsedAmount)
                .ToList();

            return new SuccessDataResult<List<IngredientUsageReportDto>>(result);
        }

    }
}
