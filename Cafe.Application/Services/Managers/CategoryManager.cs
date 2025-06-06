using AutoMapper;
using Cafe.Application.DTOs.Categories;
using Cafe.Application.DTOs.Products;
using Cafe.Application.Interfaces.Services.Contracts;
using Cafe.Application.Repositories;
using Cafe.Core.Utilities.Results;
using Cafe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Services.Managers
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryDal categoryDal, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
        }
        public async Task<IResult> AddAsync(CategoryCreateDto categoryCreateDto)
        {
            var exists = await _categoryDal.GetAsync(c => c.Name == categoryCreateDto.Name);
            if (exists != null)
                return new ErrorResult("Bu isimde bir kategori zaten var.");

            var category = _mapper.Map<Category>(categoryCreateDto);
            await _categoryDal.AddAsync(category);
            return new SuccessResult("Kategori başarıyla eklendi.");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var category = await _categoryDal.GetWithProductsByIdAsync(id);
            if (category == null)
                return new ErrorResult("Kategori bulunamadı.");

            if (category.Products.Any())
                return new ErrorResult("Bu kategoriye bağlı ürünler var, silinemez.");

            await _categoryDal.DeleteAsync(category);
            return new SuccessResult("Kategori silindi.");
        }

        public async Task<IDataResult<List<CategoryGetDto>>> GetAllAsync()
        {
            var categories = await _categoryDal.GetAllWithProductsAsync();

            var dtoList = categories.Select(category =>
            {
                var dto = _mapper.Map<CategoryGetDto>(category);
                dto.Products = _mapper.Map<List<ProductGetDto>>(category.Products);
                return dto;
            }).ToList();

            return new SuccessDataResult<List<CategoryGetDto>>(dtoList);

        }

        public async Task<IDataResult<CategoryGetDto?>> GetByIdAsync(int id)
        {
            var category = await _categoryDal.GetWithProductsByIdAsync(id);
            if (category == null)
                return new ErrorDataResult<CategoryGetDto?>("Kategori bulunamadı");

            var dto = _mapper.Map<CategoryGetDto>(category);
            dto.Products = _mapper.Map<List<ProductGetDto>>(category.Products);
            return new SuccessDataResult<CategoryGetDto?>(dto);
        }

        public async Task<IDataResult<List<CategoryGetDto>>> SearchAsync(string keyword)
        {
            var categories = await _categoryDal.GetAllWithProductsAsync();

            var filtered = categories
                .Where(c => c.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .Select(c =>
                {
                    var dto = _mapper.Map<CategoryGetDto>(c);
                    dto.Products = _mapper.Map<List<ProductGetDto>>(c.Products);
                    return dto;
                })
                .ToList();

            return new SuccessDataResult<List<CategoryGetDto>>(filtered);
        }

        public async Task<IResult> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _categoryDal.GetAsync(c => c.Id == categoryUpdateDto.Id);
            if (category == null)
                return new ErrorResult("Kategori bulunamadı.");

            category.Name = categoryUpdateDto.Name;
            await _categoryDal.UpdateAsync(category);
            return new SuccessResult("Kategori başarıyla güncellendi.");
        }
    }
}
