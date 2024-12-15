using EntityFramework.Enities;

namespace EntityFramework.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity of type T.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all entities of type T from the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of entities.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Adds a new entity to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added entity of type T.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity with updated values.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Deletes an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(int id);
    }
}
