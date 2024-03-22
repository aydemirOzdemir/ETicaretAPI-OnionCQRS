using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Features.Brands.CreateBrand;

public class CreateBrandCommandRequest:IRequest<Unit>
{
    public string Name { get; set; }
}
