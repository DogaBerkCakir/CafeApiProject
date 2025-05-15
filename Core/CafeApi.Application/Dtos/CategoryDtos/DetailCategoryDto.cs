using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.MenuItemDtos;

namespace CafeApi.Application.Dtos.CategoryDtos
{
    class DetailCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ResultMenuItemDto> menuItems { get; set; }
    }
}
