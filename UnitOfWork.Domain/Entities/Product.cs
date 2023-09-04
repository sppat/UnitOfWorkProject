using UnitOfWork.Domain.Dtos.Product;
using UnitOfWork.Domain.ValueObjects.Product;

namespace UnitOfWork.Domain.Entities
{
    public class Product
    {
        public ProductId Id { get; init; }

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        private Product()
        {
        }

        private Product(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public static Product Create(CreateProductDto dto) => new(dto.Name, dto.Description);

        public static void Update(Product product, UpdateProductDto dto)
        {
            var (name, description) = dto;

            product.Name = name ?? product.Name;
            product.Description = description ?? product.Description;
        }
    }
}
