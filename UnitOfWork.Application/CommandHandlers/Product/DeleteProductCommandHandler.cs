using MediatR;
using UnitOfWork.Application.Commands.Product;
using UnitOfWork.Application.Interfaces.Repositories;

namespace UnitOfWork.Application.CommandHandlers.Product
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.ProductRepository.DeleteAsync(request.Id);

                _unitOfWork.Save();

                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not delete product. Reason: {ex.Message}");
            }
        }
    }
}
