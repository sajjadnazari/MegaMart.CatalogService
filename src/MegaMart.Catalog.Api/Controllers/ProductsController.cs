using MediatR;
using MegaMart.Catalog.Application.Features.Products.Commands;
using MegaMart.Catalog.Application.Features.Products.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MegaMart.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var productId = await sender.Send(command);

            // برگرداندن کد 201 Created به همراه شناسه محصول
            return CreatedAtAction(nameof(CreateProduct), new { id = productId }, productId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var products = await sender.Send(query);

            // برگرداندن کد 200 به همراه لیست محصولات
            return Ok(products);
        }
    }
}
