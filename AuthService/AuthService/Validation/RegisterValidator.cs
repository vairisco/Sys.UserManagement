using AuthService.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Validation
{
    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} không được để trống!")
                .EmailAddress().WithMessage("Không đúng định dạng email.");
            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("{PropertyName} không được để trống!");
            RuleFor(p => p.Password).NotEmpty().WithMessage("{PropertyName} không được để trống!");
        }
    }
}
