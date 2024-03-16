using E_Ticaret.Application.Interfaces.AutoMapper;
using E_Ticaret.Application.UnitOfWorks;
using E_Ticaret.Domain.Entities;
using MediatR;

namespace E_Ticaret.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        Product product= await unitOfWork.GetReadRepository<Product>().GetAsync(x=>x.Id==request.Id && !x.IsDeleted);
        Product map=mapper.Map<Product,UpdateProductCommandRequest>(request);
        var productCategories = await unitOfWork.GetReadRepository<ProductCategory>().GetAllAsync(x => x.ProductId == product.Id);
        await unitOfWork.GetWriteRepository<ProductCategory>().DeleteRangeAsync(productCategories);
        foreach (var categoryId in request.CategoryIds)
            await unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new ProductCategory { CategoryId = categoryId, ProductId = product.Id });
        await unitOfWork.GetWriteRepository<Product>().UpdateAsync(map);
        await unitOfWork.SaveAsync();
    }
}
