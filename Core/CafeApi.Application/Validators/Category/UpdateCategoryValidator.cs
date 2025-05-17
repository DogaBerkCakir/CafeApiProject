using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.CategoryDtos;
using FluentValidation;
using FluentValidation.Validators;

namespace CafeApi.Application.Validators.Category
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("isim girmek zorunludur....")
                .MaximumLength(50).WithMessage("En fazla 50 karakter kullanılabilir...")
                .MinimumLength(2).WithMessage("En az 2 karakter kullanılmalıdır...");
        }
    }
    
}
