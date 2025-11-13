namespace Bookstore.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorsRepository Authors { get; }
        IAwardsRepository Awards { get; }
        IBooksRepository Books { get; }
        IComicsRepository Comics { get; }
        IPublishersRepository Publishers { get; }
        IReviewRepository Reviews { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task<int> CompleteAsync();
    }
}
