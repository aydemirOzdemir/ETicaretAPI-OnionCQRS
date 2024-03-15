using E_Ticaret.Application.DTOs;

namespace E_Ticaret.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductQueryResponse
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public BrandDTO Brand { get; set; }
}