using E_Ticaret.Application.RedisCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Features.Brands.Queries.GetAllBrands;

public class GetAllBrandsQueryRequest : IRequest<IList<GetAllBrandsQueryResponse>>, ICacheableQuery
{
    public string CacheKey => "GetAllBrands";

    public double CacheTime => 5;
}
