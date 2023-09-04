using MediatR;
using UnitOfWork.Application.Commands.Product;
using UnitOfWork.Application.Dtos.Product;
using UnitOfWork.Application.Extensions.Product;
using UnitOfWork.Application.Interfaces.Repositories;
using UnitOfWork.Domain.Dtos.Product;

namespace UnitOfWork.Application.CommandHandlers.Product
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductReadDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductReadDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var dto = new CreateProductDto(request.Name, request.Description);

            try
            {
                var product = Domain.Entities.Product.Create(dto);

                await _unitOfWork.ProductRepository.CreateAsync(product);
                _unitOfWork.Save();

                return product.AsReadDto();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not create product. Reason: {ex.Message}");
            }
        }
    }
}
 