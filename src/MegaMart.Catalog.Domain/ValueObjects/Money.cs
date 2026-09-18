namespace MegaMart.Catalog.Domain.ValueObjects
{
    public record Money(decimal Amount, string Currency)
    {
        // یک مقدار پیش‌فرض کاربردی (مثلاً برای ایران با واحد تومان)
        public static Money Default => new(0, "IRT");

        // متدهایی برای عملیات مالی می‌توانند اینجا اضافه شوند
        public Money Add(Money addition)
        {
            if (Currency != addition.Currency)
                throw new InvalidOperationException("ارزها باید یکسان باشند.");

            return new Money(Amount + addition.Amount, Currency);
        }
    }
}
