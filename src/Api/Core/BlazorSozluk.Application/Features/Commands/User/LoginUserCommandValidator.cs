using BlazorSozluk.Common.Models.ReauestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Application.Features.Commands.User
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator() 
        {
            RuleFor(i => i.EmailAddress)
                .NotNull()
                .EmailAddress()
                .WithMessage("{PropertyName} geçersiz mail adresi");

            RuleFor(i => i.Password)
                .NotNull()
                .MinimumLength(6).WithMessage("{PropertyName} en az {MinLenght} karakter olmalı");
        }
    }
}
