using MegaMart.Catalog.Application.Contracts;
using MegaMart.Catalog.Domain.Entities;
using MegaMart.Catalog.Infrastructure.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MegaMart.Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IOptions<MongoDbSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _products = mongoDatabase.GetCollection<Product>(settings.Value.ProductsCollectionName);
        }

        public async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _products.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _products.Find(_ => true).ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _products.InsertOneAsync(product, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _products.ReplaceOneAsync(x => x.Id == product.Id, product, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _products.DeleteOneAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }
    }
}
