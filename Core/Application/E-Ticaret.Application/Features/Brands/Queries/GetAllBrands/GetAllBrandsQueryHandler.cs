using E_Ticaret.Application.Bases;
using E_Ticaret.Application.Interfaces.AutoMapper;
using E_Ticaret.Application.UnitOfWorks;
using E_Ticaret.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace E_Ticaret.Application.Features.Brands.Queries.GetAllBrands;

public class GetAllBrandsQueryHandler : BaseHandler,IRequestHandler<GetAllBrandsQueryRequest, IList<GetAllBrandsQueryResponse>>
{
    public GetAllBrandsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
    {
    }

    public async Task<IList<GetAllBrandsQueryResponse>> Handle(GetAllBrandsQueryRequest request, CancellationToken cancellationToken)
    {
        var brands = await unitOfWork.GetReadRepository<Brand>().GetAllAsync();
        return mapper.Map<GetAllBrandsQueryResponse, Brand>(brands);
    }
}
