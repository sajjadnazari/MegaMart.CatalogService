using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MegaMart.Catalog.Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // 1. اگر خطا از نوع Validation بود (کد 400)
            if (exception is ValidationException validationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                var validationProblemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "خطای اعتبارسنجی اطلاعات",
                    Detail = "دیتای ارسالی با قوانین سیستم مغایرت دارد."
                };

                // گروه‌بندی خطاها بر اساس نام فیلد (مثلاً PriceAmount)
                var errors = validationException.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage).ToArray()
                    );

                validationProblemDetails.Extensions.Add("errors", errors);

                await httpContext.Response.WriteAsJsonAsync(validationProblemDetails, cancellationToken);
                return true; // به سیستم می‌گوییم خطا هندل شد
            }

            // 2. مدیریت سایر خطاهای پیش‌بینی نشده (کد 500)
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var serverErrorProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "خطای داخلی سرور",
                Detail = "یک خطای غیرمنتظره رخ داد."
            };

            await httpContext.Response.WriteAsJsonAsync(serverErrorProblemDetails, cancellationToken);
            return true;
        }
    }
}
