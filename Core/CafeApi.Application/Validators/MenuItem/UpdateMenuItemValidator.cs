using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.MenuItemDtos;
using FluentValidation;

namespace CafeApi.Application.Validators.MenuItem
{
    public class UpdateMenuItemValidator : AbstractValidator<UpdateMenuItemDto>
    {
        public UpdateMenuItemValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("MenuItem adı boş olamaz")
                .MinimumLength(3)
                .WithMessage("MenuItem adı en az 3 karakter olmalıdır");
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("MenuItem fiyatı boş olamaz")
                .GreaterThan(0)
                .WithMessage("MenuItem fiyatı 0'dan büyük olmalıdır");
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("MenuItem kategorisi boş olamaz");
        }
    }
    
}
