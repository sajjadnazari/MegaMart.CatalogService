using FluentValidation;
using MediatR;

namespace MegaMart.Catalog.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                // اجرای تمام Validatorهای ثبت شده برای این ریکوئست
                var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                // جمع‌آوری خطاها
                var failures = validationResults
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .ToList();

                // اگر خطایی بود، اصلاً به مرحله بعد (next) نمی‌رویم و اکسپشن پرت می‌کنیم
                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }

            // اگر دیتای ورودی سالم بود، برو سراغ هندلر اصلی
            return await next();
        }
    }
}
