using E_Ticaret.Application.Features.Products.Commands.CreateProduct;
using E_Ticaret.Application.Features.Products.Commands.DeleteProduct;
using E_Ticaret.Application.Features.Products.Commands.UpdateProduct;
using E_Ticaret.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()=> Ok(await mediator.Send(new GetAllProductsQueryRequest()));
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }

    }
}
