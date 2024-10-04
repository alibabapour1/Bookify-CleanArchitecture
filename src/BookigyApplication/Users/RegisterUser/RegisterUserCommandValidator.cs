﻿using System.Data;
using FluentValidation;

namespace Bookify.Application.Users.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FirstName ).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.EmailAddress).EmailAddress();
        RuleFor(x=> x.Password).NotEmpty().MinimumLength(6);
    }
}