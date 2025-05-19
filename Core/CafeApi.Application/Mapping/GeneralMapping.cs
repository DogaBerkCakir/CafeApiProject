using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CafeApi.Application.Dtos.CategoryDtos;
using CafeApi.Application.Dtos.MenuItemDtos;
using CafeApi.Application.Dtos.OrderDtos;
using CafeApi.Application.Dtos.OrderItemDtos;
using CafeApi.Application.Dtos.TableDtos;
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

            CreateMap<MenuItem, ResultMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, CreateMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, DetailMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, UpdateMenuItemDto>().ReverseMap();

            CreateMap<Table, ResultTableDto>().ReverseMap();
            CreateMap<Table, DetailTableDto>().ReverseMap();
            CreateMap<Table, UpdateTableDto>().ReverseMap();
            CreateMap<Table, CreateTableDto>().ReverseMap();

            CreateMap<Order, ResultOrderDto>().ReverseMap();
            CreateMap<Order, DetailOrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();

            CreateMap<OrderItem, ResultOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, DetailOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, CreateOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, UpdateOrderItemDto>().ReverseMap();






        }
    }
}
