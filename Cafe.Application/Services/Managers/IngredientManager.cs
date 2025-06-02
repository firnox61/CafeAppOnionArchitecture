using AutoMapper;
using Cafe.Application.DTOs.Ingredients;
using Cafe.Application.Interfaces.Services.Contracts;
using Cafe.Application.Repositories;
using Cafe.Application.Utilities.Results;
using Cafe.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Services.Managers
{
    public class IngredientManager : IIngredientService
    {
        private readonly IIngredientDal _ingredientDal;
        private readonly IMapper _mapper;

        public IngredientManager(IIngredientDal ıngredientDal, IMapper mapper)
        {
            _ingredientDal = ıngredientDal;
            _mapper = mapper;
        }
        // [CacheRemoveAspect("ITableService.Get")]
        //  [ValidationAspect(typeof(IngredientCreateDtoValidator))]
        //    [LogAspect(typeof(FileLogger))] // opsiyonel: loglama da ekli
        //  [CacheRemoveAspect("CafeApp.Business.Concrete.IngredientManager.GetAllAsync")]
        public async Task<IResult> Add(IngredientCreateDto ingredientCreateDto)
        {
            // Saf ekleme — yalnızca yeni kayıt
            var existingIngredient = await _ingredientDal.GetAsync(i => i.Name.ToLower() == ingredientCreateDto.Name.ToLower());
            if (existingIngredient != null)
                return new ErrorResult("Bu malzeme zaten var. Lütfen stok artırmak için ilgili işlemi kullanın.");


            var newIngredient = _mapper.Map<Ingredient>(ingredientCreateDto);
            await _ingredientDal.AddAsync(newIngredient);
            return new SuccessResult();
        }
        public async Task<IResult> IncreaseStockAsync(int ingredientId, int quantityToAdd)
        {
            var ingredient = await _ingredientDal.GetAsync(i => i.Id == ingredientId);

            if (ingredient == null)
                return new ErrorResult("Malzeme bulunamadı.");

            if (quantityToAdd <= 0)
                return new ErrorResult("Eklenecek miktar 0'dan büyük olmalıdır.");

            ingredient.Stock += quantityToAdd;
            await _ingredientDal.UpdateAsync(ingredient);

            return new SuccessResult("Malzeme stoğu güncellendi.");
        }
        public async Task<IResult> Delete(int ingredientId)
        {
            var ingredient = await _ingredientDal.GetAsync(p => p.Id == ingredientId);
            if (ingredient == null)
                return new ErrorResult("Malzeme bulunamadı.");

            await _ingredientDal.DeleteAsync(ingredient);
            return new SuccessResult("Malzeme silindi.");
        }
        //  [CacheAspect(duration: 10)]
        public async Task<IDataResult<List<IngredientDto>>> GetAllAsync()
        {
            var ingredientEntities = await _ingredientDal.GetAllAsync();
            var dtoList = _mapper.Map<List<IngredientDto>>(ingredientEntities);

            Console.WriteLine("IngredientDto listesi başarıyla maplendi");

            return new SuccessDataResult<List<IngredientDto>>(dtoList);

        }

        public async Task<IDataResult<Ingredient?>> GetById(int id)
        {
            var Existingredient = await _ingredientDal.GetAsync(i => i.Id == id);
            if (Existingredient == null)
            {
                return new ErrorDataResult<Ingredient?>("Malzeme bulunamadı");
            }
            return new SuccessDataResult<Ingredient?>(Existingredient);
        }

        public async Task<IResult> Update(IngredientUpdateDto ingredientUpdateDto)
        {

            var existIngredient = await _ingredientDal.GetAsync(i => i.Id == ingredientUpdateDto.Id);



            if (existIngredient == null)
            {
                return new ErrorResult("Masa bulunamadı");
            }
            // DTO'daki değerleri mevcut entity'e uygula
            _mapper.Map(ingredientUpdateDto, existIngredient);
            await _ingredientDal.UpdateAsync(existIngredient);
            return new SuccessResult();
        }
        public async Task<IDataResult<List<StockAlertDto>>> GetCriticalStockIngredientsAsync()
        {
            var allIngredients = await _ingredientDal.GetAllAsync(i => i.Stock <= i.MinStockThreshold);
            var dtoList = _mapper.Map<List<StockAlertDto>>(allIngredients);
            if (dtoList == null || !dtoList.Any())
            {
                return new SuccessDataResult<List<StockAlertDto>>("Kritik stok seviyesinde herhangi bir malzeme bulunmamaktadır");
            }

            return new SuccessDataResult<List<StockAlertDto>>(dtoList, "Kritik seviyenin altına düşen malzemeler");
        }
    }
}
