namespace UnitOfWork.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        void Dispose();
        void Save();
    }
}
