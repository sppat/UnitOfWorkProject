using UnitOfWork.Domain.Entities;

namespace UnitOfWork.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetProductByIdAsync(Guid productId);
        Task<bool> CreateAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
