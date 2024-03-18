using E_Ticaret.Application.Features.Products.Rules;
using E_Ticaret.Application.Interfaces.AutoMapper;
using E_Ticaret.Application.UnitOfWorks;
using E_Ticaret.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest,Unit>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ProductRules productRules;

    public CreateProductCommandHandler(IMapper mapper,IUnitOfWork unitOfWork,ProductRules productRules)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.productRules = productRules;
    }
    public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        IList<Product> products=await unitOfWork.GetReadRepository<Product>().GetAllAsync();
        await productRules.ProductTitleMustNotBeSame(request.Title,products);
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
        return Unit.Value;
    }
}

