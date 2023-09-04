using MediatR;
using UnitOfWork.Application.Dtos.Product;
using UnitOfWork.Application.Extensions.Product;
using UnitOfWork.Application.Interfaces.Repositories;
using UnitOfWork.Application.Queries.Product;

namespace UnitOfWork.Application.QueryHandlers.Product
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductReadDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductReadDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return (await _unitOfWork.ProductRepository
                    .GetAllAsync())
                    .Select(p => p.AsReadDto());
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not retrieve products. Reason: {ex.Message}");
            }
            finally 
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
