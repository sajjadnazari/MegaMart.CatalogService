using FluentValidation;

namespace MegaMart.Catalog.Application.Features.Products.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("نام محصول الزامی است.")
                .MaximumLength(200).WithMessage("نام محصول نباید بیشتر از 200 کاراکتر باشد.");

            RuleFor(p => p.PriceAmount)
                .GreaterThanOrEqualTo(0).WithMessage("قیمت نمی‌تواند منفی باشد.");

            RuleFor(p => p.PriceCurrency)
                .NotEmpty().WithMessage("واحد پول باید مشخص شود.");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("شناسه دسته‌بندی نامعتبر است.");
        }
    }
}
