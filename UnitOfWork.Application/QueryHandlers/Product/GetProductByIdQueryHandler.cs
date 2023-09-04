using MediatR;
using UnitOfWork.Application.Dtos.Product;
using UnitOfWork.Application.Extensions.Product;
using UnitOfWork.Application.Interfaces.Repositories;
using UnitOfWork.Application.Queries.Product;

namespace UnitOfWork.Application.QueryHandlers.Product
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductReadDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductReadDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return (await _unitOfWork.ProductRepository
                    .GetProductByIdAsync(request.Id))?
                    .AsReadDto();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not retrieve product. Reason: {ex.Message}");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
