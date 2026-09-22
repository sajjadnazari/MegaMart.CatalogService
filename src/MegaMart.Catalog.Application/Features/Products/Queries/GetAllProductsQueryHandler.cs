using MediatR;
using MegaMart.Catalog.Application.Contracts;
using MegaMart.Catalog.Application.DTOs;

namespace MegaMart.Catalog.Application.Features.Products.Queries
{
    public class GetAllProductsQueryHandler(IProductRepository repository) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            // دریافت دیتا از دیتابیس
            var products = await repository.GetAllAsync(cancellationToken);

            // Map کردن Entity به DTO
            return products.Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Description,
                p.Price.Amount,
                p.Price.Currency,
                p.Attributes
            ));
        }
    }
}
