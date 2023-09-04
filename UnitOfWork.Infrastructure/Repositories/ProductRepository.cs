using Dapper;
using System.Data;
using UnitOfWork.Application.Interfaces.Repositories;
using UnitOfWork.Domain.Entities;

namespace UnitOfWork.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public ProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> CreateAsync(Product product)
        {
            const string query = @"
                INSERT INTO products (id, name, description)
                VALUES (@Id, @Name, @Description)
            ";

            return await _connection.ExecuteAsync(query, new { product.Id, product.Name, product.Description }) == 1;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            const string query = @"
                SELECT * 
                FROM products
            ";
            
            return await _connection.QueryAsync<Product>(query);
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            const string query = @"
                SELECT *
                FROM products
                WHERE id = @Id
            ";

            return await _connection.QuerySingleOrDefaultAsync<Product>(query, new { Id = productId });
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            const string query = @"
                UPDATE products
                SET name = @Name, description = @Description
                WHERE id = @Id
            ";

            return await _connection.ExecuteAsync(query, new { product.Name, product.Description, product.Id }) == 1;
        }

        public async Task DeleteAsync(Guid productId)
        {
            const string query = @"
                DELETE FROM products
                WHERE id = @Id
            ";

            await _connection.ExecuteAsync(query, new { Id = productId });
        }
    }
}
