using MediatR;
using UnitOfWork.Application.Dtos.Product;
using UnitOfWork.Domain.Dtos.Product;

namespace UnitOfWork.Application.Commands.Product
{
    public record UpdateProductCommand(Guid Id, UpdateProductDto Dto) : IRequest<ProductReadDto>;
}
