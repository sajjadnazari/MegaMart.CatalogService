using MediatR;
using MegaMart.Catalog.Application.Contracts;
using MegaMart.Catalog.Domain.Entities;
using MegaMart.Catalog.Domain.ValueObjects;

namespace MegaMart.Catalog.Application.Features.Products.Commands
{
    public class CreateProductCommandHandler(IProductRepository repository) : IRequestHandler<CreateProductCommand, Guid>
    {
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // 1. تبدیل دیتای ورودی به Value Object دامین
            var price = new Money(request.PriceAmount, request.PriceCurrency);

            // 2. ساخت Entity با استفاده از Factory Method
            var product = Product.Create(
                request.Name,
                request.Description,
                price,
                request.CategoryId,
                request.Attributes);

            // 3. ذخیره در دیتابیس (MongoDB)
            await repository.AddAsync(product, cancellationToken);

            // 4. بازگرداندن شناسه تولید شده
            return product.Id;
        }
    }
}
