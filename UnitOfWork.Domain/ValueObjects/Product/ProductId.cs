namespace UnitOfWork.Domain.ValueObjects.Product
{
    public record ProductId
    {
        public Guid Value { get; init; }

        public ProductId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new Exception("Id cannot be empty.");
            }

            Value = id;
        }

        public static implicit operator ProductId(Guid id) => new(id);
        public static implicit operator Guid(ProductId id) => id.Value;
    }
}
