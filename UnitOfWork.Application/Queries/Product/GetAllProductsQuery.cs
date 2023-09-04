using MediatR;
using UnitOfWork.Application.Dtos.Product;

namespace UnitOfWork.Application.Queries.Product
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<ProductReadDto>>;
}
