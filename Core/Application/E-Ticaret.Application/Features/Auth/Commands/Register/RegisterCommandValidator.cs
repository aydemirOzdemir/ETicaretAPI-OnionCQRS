using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator:AbstractValidator<RegisterCommandRequest>
{
    public RegisterCommandValidator()
    {
        RuleFor(x=>x.FullName).NotEmpty().MaximumLength(50).MinimumLength(2).WithName("İsim Soyİsim");
        RuleFor(x=>x.Email).NotEmpty().MaximumLength(60).EmailAddress().MinimumLength(8).WithName("Email Adres");
        RuleFor(x=>x.Password).NotEmpty().MinimumLength(6).WithName("Şifre");
        RuleFor(x=>x.ConfirmPassword).NotEmpty().MinimumLength(6).Equal(x=>x.Password).WithName("Şifre Tekrarı");
    }
}
