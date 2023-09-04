using UnitOfWork.Application.Dtos.Product;

namespace UnitOfWork.Application.Extensions.Product
{
    public static class ProductExtensions
    {
        public static ProductReadDto AsReadDto(this Domain.Entities.Product product)
            => new(product.Id, product.Name, product.Description);
    }
}
