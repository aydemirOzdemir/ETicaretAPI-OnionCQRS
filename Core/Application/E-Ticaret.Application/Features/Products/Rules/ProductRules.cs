using E_Ticaret.Application.Bases;
using E_Ticaret.Application.Features.Products.Exceptions;
using E_Ticaret.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Features.Products.Rules;

public class ProductRules:BaseRules
{
    public Task ProductTitleMustNotBeSame(string requesttitle,IList<Product> products)
    {
        if (products.Any(x=>x.Title==requesttitle))
            throw new ProductTitleMustNotBeSameException();
        return Task.CompletedTask;
    }
}
