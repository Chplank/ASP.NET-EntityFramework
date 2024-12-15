using EntityFramework.Enities;
using EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Repository
{
    /// <summary>
    /// A generic repository implementation for performing CRUD operations on entities.
    /// </summary>
    /// <typeparam name="TValue">The type of the entity, must inherit from BaseEntity.</typeparam>
    public class DbRepository<TValue> : IRepository<TValue> where TValue : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TValue> _DBSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbRepository{TValue}"/> class.
        /// </summary>
        /// <param name="context">The DbContext used for database operations.</param>
        public DbRepository(DbContext context)
        {
            _context = context;
            _DBSet = context.Set<TValue>();
        }

        /// <summary>
        /// Adds a new entity to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
        public async Task<TValue> AddAsync(TValue entity)
        {
            if (_DBSet.Contains(entity)) return entity;

            await _DBSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Deletes an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the ID is invalid.</exception>
        public async Task DeleteAsync(int id)
        {
            if (id == null)
            {
                throw new InvalidOperationException();
            }

            await _DBSet.Where(t => t.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves all entities of type TValue from the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of entities.</returns>
        public async Task<IEnumerable<TValue>> GetAllAsync()
        {
            return await _DBSet.ToListAsync();
        }

        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity of type TValue.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the ID is invalid.</exception>
        public async Task<TValue> GetByIdAsync(int id)
        {
            if (id == null)
            {
                throw new InvalidOperationException();
            }

            return await _DBSet.FindAsync(id);
        }

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity with updated values.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the entity is null.</exception>
        public async Task UpdateAsync(TValue entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _DBSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
