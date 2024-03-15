using E_Ticaret.Application.DTOs;
using E_Ticaret.Application.Interfaces.AutoMapper;
using E_Ticaret.Application.UnitOfWorks;
using E_Ticaret.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductQueryResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    public async Task<IList<GetAllProductQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        IList<Product> products = await unitOfWork.GetReadRepository<Product>().GetAllAsync(include:x=>x.Include(b=>b.Brand));
        mapper.Map<BrandDTO, Brand>(new Brand());
        //List<GetAllProductQueryResponse> response= new List<GetAllProductQueryResponse>();
        //foreach (Product product in products)
        //    response.Add(new GetAllProductQueryResponse
        //    {
        //        Title = product.Title,
        //        Description = product.Description,
        //        Price = product.Price-(product.Price*product.Discount/100),
        //        Discount = product.Discount,
        //    });
        var map = mapper.Map<GetAllProductQueryResponse, Product>(products);
        foreach (var item in map)
            item.Price -= (item.Price * item.Discount / 100);
        return map;
    }
}