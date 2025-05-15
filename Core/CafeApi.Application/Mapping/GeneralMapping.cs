using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CafeApi.Application.Dtos.CategoryDtos;
using CafeApi.Application.Dtos.MenuItemDtos;
using CafeApi.Domain.Entities;

namespace CafeApi.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category,ResultCategoryDto>().ReverseMap();
            CreateMap<Category,DetailCategoryDto>().ReverseMap();
            CreateMap<Category,CreateCategoryDto>().ReverseMap();
            CreateMap<Category,UpdateCategoryDto>().ReverseMap();

            CreateMap<MenuItem,ResultMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, CreateMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, DetailMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, UpdateMenuItemDto>().ReverseMap();
        }
    }
}
