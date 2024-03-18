using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator:AbstractValidator<CreateProductCommandRequest>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithName("Başlık");
        RuleFor(x => x.Description).NotEmpty().WithName("Açıklama");
        RuleFor(x => x.BrandId).GreaterThan(0).WithName("Marka");
        RuleFor(x => x.Price).GreaterThan(0).WithName("Ücret");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithName("İndirim Oranı");
        RuleFor(x => x.CategoryIds).NotEmpty().Must(categories=>categories.Any()).WithName("Kategoriler");
    }
}
