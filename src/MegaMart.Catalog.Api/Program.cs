using FluentValidation;
using MegaMart.Catalog.Application.Behaviors;
using MegaMart.Catalog.Application.Contracts;
using MegaMart.Catalog.Infrastructure.Data;
using MegaMart.Catalog.Infrastructure.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

var builder = WebApplication.CreateBuilder(args);
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
// تنظیمات MongoDB
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddValidatorsFromAssembly(typeof(MegaMart.Catalog.Application.Features.Products.Commands.CreateProductCommand).Assembly);
// 2. تغییر نحوه ثبت MediatR برای اضافه کردن Behavior
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(MegaMart.Catalog.Application.Features.Products.Commands.CreateProductCommand).Assembly);
    // اضافه کردن دروازه‌بان اعتبارسنجی به پایپ‌لاین
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
// ثبت Repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
