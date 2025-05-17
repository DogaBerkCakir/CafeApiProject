using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CafeApi.Application.Dtos.MenuItemDtos;
using CafeApi.Application.Dtos.ResponseDtos;
using CafeApi.Application.Interfaces;
using CafeApi.Application.Services.Abstract;
using CafeApi.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CafeApi.Application.Services.Concrete
{
    public class MenuItemServices : IMenuItemServices
    {
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateMenuItemDto> _createMenuItemValidator;
        private readonly IValidator<UpdateMenuItemDto> _updateMenuItemValidator;
        public MenuItemServices(IGenericRepository<MenuItem> menuItemRepository, IMapper mapper, IValidator<CreateMenuItemDto> createMenuItemValidator, IValidator<UpdateMenuItemDto> updateMenuItemValidator)
        {
            _mapper = mapper;
            _menuItemRepository = menuItemRepository;
            _createMenuItemValidator = createMenuItemValidator;
            _updateMenuItemValidator = updateMenuItemValidator;
        }
        public async Task<ResponseDto<object>> AddMenuItem(CreateMenuItemDto dto)
        {
            try
            {
                var validationResult = await _createMenuItemValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join(".",validationResult.Errors.Select(x => x.ErrorMessage)),
                        ErrorCodes = ErrorCodes.ValidationError
                    };
                }

                var menuItem = _mapper.Map<MenuItem>(dto);
                await _menuItemRepository.AddAsync(menuItem);
                if (menuItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "MenuItem eklenemedi...",
                        ErrorCodes = ErrorCodes.Exception
                    };
                }
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "MenuItem eklendi...",
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Success = false,
                    Message = "Bir hata oluştu...",
                    ErrorCodes = ErrorCodes.Exception
                };
            }

        }

        public  async Task<ResponseDto<object>> DeleteMenuItem(int id)
        {
            var menuItem =  await _menuItemRepository.GetByIdAsync(id);
            if (menuItem == null)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "MenuItem bulunamadi...",
                    ErrorCodes = ErrorCodes.NotFound
                };
            }
            await _menuItemRepository.DeleteAsync(menuItem);
            return new ResponseDto<object>
            {
                Success = true,
                Data = null,
                Message = "MenuItem silindi..."
            };
        }

        public async Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems()
        {
            try
            {
                var menuItems = await _menuItemRepository.GetAllAsync();
                if (menuItems.Count == 0)
                {
                    return new ResponseDto<List<ResultMenuItemDto>>
                    {
                        Success = false,
                        Data = null,
                        Message = "MenuItems bulunamadi....",
                        ErrorCodes = ErrorCodes.NotFound
                    };

                }
                var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
                return new ResponseDto<List<ResultMenuItemDto>>
                {
                    Success = true,
                    Data = result,
                    Message = "MenuItems Listesi..."
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultMenuItemDto>>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu..." ,
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItem(int id)
        {
            try
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(id);
                if (menuItem == null)
                {
                    return new ResponseDto<DetailMenuItemDto>
                    {
                        Success = false,
                        Data = null,
                        Message = "MenuItem bulunamadi....",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<DetailMenuItemDto>(menuItem);
                return new ResponseDto<DetailMenuItemDto>
                {
                    Success = true,
                    Data = result,
                    Message = "MenuItem Detayları..."
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailMenuItemDto>
                {
                    ErrorCodes = ErrorCodes.Exception,
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu..."
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            try
            {
                var validationResult = await _updateMenuItemValidator.ValidateAsync(dto);
                if(validationResult.IsValid == false)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join(".", validationResult.Errors.Select(x => x.ErrorMessage)),
                        ErrorCodes = ErrorCodes.ValidationError
                    };
                }
                var menuItem = await _menuItemRepository.GetByIdAsync(dto.Id);
                if (menuItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "MenuItem bulunamadi...",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }

                var newMenuItem = _mapper.Map(dto, menuItem);
                await _menuItemRepository.UpdateAsync(newMenuItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "MenuItem güncellendi..."
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu...",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }
    }
}
