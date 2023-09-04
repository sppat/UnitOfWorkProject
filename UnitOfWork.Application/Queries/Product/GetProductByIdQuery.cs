using MediatR;
using UnitOfWork.Application.Dtos.Product;

namespace UnitOfWork.Application.Queries.Product
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductReadDto?>;
}
