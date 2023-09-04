using MediatR;
using UnitOfWork.Application.Commands.Product;
using UnitOfWork.Application.Dtos.Product;
using UnitOfWork.Application.Extensions.Product;
using UnitOfWork.Application.Interfaces.Repositories;

namespace UnitOfWork.Application.CommandHandlers.Product
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductReadDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductReadDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            var dto = request.Dto;

            try
            {
                var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(id)
                    ?? throw new Exception($"Product not found. Id: {id}");

                Domain.Entities.Product.Update(product, dto);

                await _unitOfWork.ProductRepository.UpdateAsync(product);
                _unitOfWork.Save();

                return product.AsReadDto();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update product. Reason: {ex.Message}");
            }
            
            
        }
    }
}
