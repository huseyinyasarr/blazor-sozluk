using BlazorSozluk.Common.Models.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Application.Features.Commands.User.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{

    //TODO


    //public LoginUserCommandValidator()
    //{
    //    RuleFor(i => i.EmailAddress)
    //        .NotNull()
    //        .EmailAddress()
    //        .WithMessage("{PropertyName} geçersiz mail adresi");

    //    RuleFor(i => i.Password)
    //        .NotNull()
    //        .MinimumLength(6).WithMessage("{PropertyName} en az {MinLength} karakter olmalı");
    //}


    public LoginUserCommandValidator()
    {
        RuleFor(i => i.EmailAddress)
            .NotNull()
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("{PropertyName} not a valid email address");

        RuleFor(i => i.Password)
            .NotNull()
            .MinimumLength(6).WithMessage("{PropertyName} should at least be {MinLenght} characters");
    }



}
