using Dapper;
using System.Data;
using UnitOfWork.Domain.ValueObjects.Product;

namespace UnitOfWork.Infrastructure.TypeHandlers
{
    internal class ProductIdTypeHandler : SqlMapper.TypeHandler<ProductId>
    {
        public override ProductId Parse(object value)
        {
            return new((Guid)value);
        }

        public override void SetValue(IDbDataParameter parameter, ProductId value)
        {
            parameter.Value = value.Value;
        }
    }
}
