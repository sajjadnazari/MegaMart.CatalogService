namespace MegaMart.Catalog.Application.DTOs
{
    public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal PriceAmount,
    string PriceCurrency,
    Dictionary<string, string> Attributes);
}
