using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CafeApi.Application.Dtos.CategoryDtos;
using CafeApi.Application.Dtos.MenuItemDtos;
using CafeApi.Application.Dtos.ResponseDtos;
using CafeApi.Application.Interfaces;
using CafeApi.Application.Services.Abstract;
using CafeApi.Application.Validators.Category;
using CafeApi.Domain.Entities;
using FluentValidation;

namespace CafeApi.Application.Services.Concrete
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;
        private readonly IGenericRepository<MenuItem> _menuItemRepository;



        public CategoryServices(IGenericRepository<Category> categoryRepository, IMapper mapper, IValidator<CreateCategoryDto> createCategoryValidator, IValidator<UpdateCategoryDto> updateCategoryValidator, IGenericRepository<MenuItem> menuItemRepository)
        {

            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
            _menuItemRepository = menuItemRepository;
        }

        public async Task<ResponseDto<object>> AddCategory(CreateCategoryDto dto)
        {
            try
            {
                var validate = await _createCategoryValidator.ValidateAsync(dto);
                if(!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = string.Join(" | ",validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError,
                        Data = null
                    };
                }
                var category = _mapper.Map<Category>(dto);
                await _categoryRepository.AddAsync(category);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Kategori Eklendi",
                    Data = category
                };
                var result = _mapper.Map<Category>(dto);

            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu...",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Kategori Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _categoryRepository.DeleteAsync(category);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Kategori Silindi",
                    Data = category
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu....",
                    ErrorCode = ErrorCodes.Exception
                };
            }

        }

        public async Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                if (categories.Count == 0)
                {
                    return new ResponseDto<List<ResultCategoryDto>>
                    {
                        Success = false,
                        Message = "Kategori Bulunamadı",
                        ErrorCode   = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultCategoryDto>>(categories);
                return new ResponseDto<List<ResultCategoryDto>>
                {
                    Success = true,
                    Message = "Kategoriler Listelendi",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultCategoryDto>> 
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.Exception
                };
            }

        }

        public async Task<ResponseDto<DetailCategoryDto>> GetByIdCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                var menuItems = await _menuItemRepository.GetAllAsync();
                if (category == null)
                {
                    return new ResponseDto<DetailCategoryDto>
                    {
                        Success = false,
                        Message = "Kategori Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<DetailCategoryDto>(category);
                return new ResponseDto<DetailCategoryDto>
                {
                    Success = true,
                    Message = "Kategori Listelendi",
                    Data = result
                };

            }
            catch (Exception ex)
            {

               return new ResponseDto<DetailCategoryDto>
               {
                   Success = false,
                   Message = ex.Message,
                   ErrorCode = ErrorCodes.Exception
               };
            }

        }

        public async Task<ResponseDto<object>> UpdateCategory(UpdateCategoryDto dto)
        {
            try
            {
                var validate = await _updateCategoryValidator.ValidateAsync(dto);
                if(!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = string.Join(" | ", validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError,
                        Data = null
                    };
                }
                var categorydb = await _categoryRepository.GetByIdAsync(dto.Id);
                if (categorydb == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Kategori Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var category = _mapper.Map(dto,categorydb);
                await _categoryRepository.UpdateAsync(category);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Kategori Güncellendi",
                    Data = category
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<object> 
                {
                    Success = false,
                    Message = "Bir Hata Oluştu...",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
