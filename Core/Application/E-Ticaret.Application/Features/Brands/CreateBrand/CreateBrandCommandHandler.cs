using Bogus;
using E_Ticaret.Application.Bases;
using E_Ticaret.Application.Interfaces.AutoMapper;
using E_Ticaret.Application.UnitOfWorks;
using E_Ticaret.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace E_Ticaret.Application.Features.Brands.CreateBrand;

public class CreateBrandCommandHandler : BaseHandler,IRequestHandler<CreateBrandCommandRequest,Unit>
{
    public CreateBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
    {
    }

    public async Task<Unit> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
    {
        Faker faker = new("tr");
        List<Brand> brands = new();
        for (int i= 0; i < 10000000; i++)
            brands.Add(new Brand(faker.Commerce.Department(1)));
        await unitOfWork.GetWriteRepository<Brand>().AddRangeAsync(brands);
        await unitOfWork.SaveAsync();
        return Unit.Value;
    }
}
