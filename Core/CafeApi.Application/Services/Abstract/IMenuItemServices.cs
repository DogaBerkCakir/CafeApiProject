using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.MenuItemDtos;
using CafeApi.Application.Dtos.ResponseDtos;

namespace CafeApi.Application.Services.Abstract
{
    public interface IMenuItemServices
    {
        Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems();
        Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItem(int id);
        Task<ResponseDto<object>> AddMenuItem(CreateMenuItemDto dto);
        Task<ResponseDto<object>> UpdateMenuItem(UpdateMenuItemDto dto);
        Task<ResponseDto<object>> DeleteMenuItem(int id);
    }
}
