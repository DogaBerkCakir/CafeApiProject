using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.CategoryDtos;
using CafeApi.Application.Dtos.MenuItemDtos;
using FluentValidation;
using FluentValidation.Validators;

namespace CafeApi.Application.Validators.MenuItem
{
    public class CreateMenuItemValidator : AbstractValidator<CreateMenuItemDto>
    {
        public CreateMenuItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("MenuItem ismi boş olamaz")
                .MinimumLength(3)
                .WithMessage("MenuItem ismi en az 3 karakter olmalıdır");
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("MenuItem fiyatı boş olamaz")
                .GreaterThan(0)
                .WithMessage("MenuItem fiyatı 0'dan büyük olmalıdır");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("MenuItem açıklaması boş olamaz")
                .MinimumLength(10)
                .WithMessage("MenuItem açıklaması en az 10 karakter olmalıdır");

        }
    }
}
