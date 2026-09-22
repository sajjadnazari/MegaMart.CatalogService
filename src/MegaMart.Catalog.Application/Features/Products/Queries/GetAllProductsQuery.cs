using MediatR;
using MegaMart.Catalog.Application.DTOs;

namespace MegaMart.Catalog.Application.Features.Products.Queries
{
    public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;
}
