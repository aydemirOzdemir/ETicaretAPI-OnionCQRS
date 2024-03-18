using E_Ticaret.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Features.Products.Exceptions;

public class ProductTitleMustNotBeSameException:BaseExceptions
{
    public ProductTitleMustNotBeSameException():base("Ürün Başlığı zaten mevcut")
    {
        
    }
}
