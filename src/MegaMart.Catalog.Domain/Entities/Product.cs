using MegaMart.Catalog.Domain.ValueObjects;

namespace MegaMart.Catalog.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }
        public Guid CategoryId { get; private set; }
        public List<string> Images { get; private set; }

        // این دیکشنری همون قلب تپنده MongoDB است که ویژگی‌های متغیر رو توش نگه می‌داریم
        // مثلا: { "RAM": "16GB", "Color": "Black" }
        public Dictionary<string, string> Attributes { get; private set; }

        // سازنده خالی برای ابزارهای ORM یا سریالایزرهای MongoDB
        private Product()
        {
            Images = new List<string>();
            Attributes = new Dictionary<string, string>();
        }

        // متد Factory برای ایجاد محصول جدید با قوانین بیزینسی
        public static Product Create(string name, string description, Money price, Guid categoryId, Dictionary<string, string> attributes)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("نام محصول نمی‌تواند خالی باشد.");

            return new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Price = price,
                CategoryId = categoryId,
                Images = new List<string>(),
                Attributes = attributes ?? new Dictionary<string, string>()
            };
        }

        // متدهای تغییر وضعیت (State Mutations)
        public void UpdatePrice(Money newPrice)
        {
            // اینجا می‌توانیم لاجیک بذاریم، مثلا قیمت جدید نباید کمتر از صفر باشه
            if (newPrice.Amount < 0)
                throw new ArgumentException("قیمت نمی‌تواند منفی باشد.");

            Price = newPrice;
        }

        public void AddImage(string imageUrl)
        {
            Images.Add(imageUrl);
        }
    }
}
