using MegaMart.Catalog.Application.Contracts;
using MegaMart.Catalog.Infrastructure.Data;
using MegaMart.Catalog.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// تنظیمات MongoDB
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

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
