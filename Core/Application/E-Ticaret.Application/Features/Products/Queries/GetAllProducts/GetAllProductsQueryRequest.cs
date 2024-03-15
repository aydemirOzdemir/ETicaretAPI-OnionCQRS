using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryRequest:IRequest<IList<GetAllProductQueryResponse>>
{
}
