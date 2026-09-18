using MediatR;
using MegaMart.Catalog.Application.Features.Products.Commands;
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
    }
}
