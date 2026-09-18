using MediatR;

namespace MegaMart.Catalog.Application.Features.Products.Commands
{
    public record CreateProductCommand(
        string Name,
        string Description,
        decimal PriceAmount,
        string PriceCurrency,
        Guid CategoryId,
        Dictionary<string, string> Attributes) : IRequest<Guid>;
}
