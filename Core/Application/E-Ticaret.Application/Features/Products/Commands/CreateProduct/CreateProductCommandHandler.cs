using E_Ticaret.Application.Interfaces.AutoMapper;
using E_Ticaret.Application.UnitOfWorks;
using E_Ticaret.Domain.Entities;
using MediatR;

namespace E_Ticaret.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public CreateProductCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }
    public async Task Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        Product product = new(request.Title, request.Description, request.BrandId, request.Price, request.Discount);
        await unitOfWork.GetWriteRepository<Product>().AddAsync(product);
        if(await unitOfWork.SaveAsync() > 0)
        {
            foreach (var catgoryId in request.CategoryIds)
            {
                await unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new ProductCategory
                {
                    ProductId=product.Id,
                    CategoryId=catgoryId
                });
                await unitOfWork.SaveAsync();
            }
        }
    }
}

