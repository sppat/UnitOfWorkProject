using MediatR;

namespace UnitOfWork.Application.Commands.Product
{
    public record DeleteProductCommand(Guid Id) : IRequest<Unit>;
}
