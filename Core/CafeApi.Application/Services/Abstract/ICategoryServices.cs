using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.CategoryDtos;

namespace CafeApi.Application.Services.Abstract
{
    public interface ICategoryServices
    {
        Task<List<ResultCategoryDto>> GetAllCategories();
        Task<DetailCategoryDto> GetByIdCategory(int id);
        Task AddCategory(CreateCategoryDto dto);
        Task UpdateCategory(UpdateCategoryDto dto);
        Task DeleteCategory(int id);
    }
}
