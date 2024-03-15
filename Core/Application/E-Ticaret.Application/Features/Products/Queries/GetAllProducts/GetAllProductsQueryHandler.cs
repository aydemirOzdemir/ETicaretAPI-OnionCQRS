using E_Ticaret.Application.UnitOfWorks;
using E_Ticaret.Domain.Entities;
using MediatR;

namespace E_Ticaret.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductQueryResponse>>
{
    private readonly IUnitOfWork unitOfWork;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public async Task<IList<GetAllProductQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        IList<Product> products = await unitOfWork.GetReadRepository<Product>().GetAllAsync();
        List<GetAllProductQueryResponse> response= new List<GetAllProductQueryResponse>();
        foreach (Product product in products)
            response.Add(new GetAllProductQueryResponse
            {
                Title = product.Title,
                Description = product.Description,
                Price = product.Price-(product.Price*product.Discount/100),
                Discount = product.Discount,
            });
       
        return response;
    }
}