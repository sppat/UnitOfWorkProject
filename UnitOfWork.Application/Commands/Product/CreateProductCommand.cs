using MediatR;
using UnitOfWork.Application.Dtos.Product;

namespace UnitOfWork.Application.Commands.Product
{
    public record CreateProductCommand(string Name, string Description) : IRequest<ProductReadDto>;
}
