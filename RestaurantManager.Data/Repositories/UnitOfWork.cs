namespace RestaurantManager.Repositories
{
    public interface IUnitOfWork<T>
    {
        /// <summary>
        /// Commits all changes made in the context to the database asynchronously.
        /// </summary>
        Task CompleteAsync();
    }

    /// <summary>
    /// Implements the unit of work pattern for a given DbContext.
    /// Ensures that all changes are saved as a single transaction.
    /// </summary>
    /// <typeparam name="T">The type of the DbContext.</typeparam>
    public class UnitOfWork<T> : IUnitOfWork<T> where T : DbContext
    {
        private readonly T _dbContext;

        public UnitOfWork(T dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public Task CompleteAsync() => _dbContext.SaveChangesAsync();
    }
}