namespace Service.Database.Operations
{
    /// <summary>
    /// Interface for database operations to create entities of type T.
    /// </summary>
    /// <typeparam name="T">The type of entity to create.</typeparam>
    public interface ICreateOperation<T> where T : class
    {
        /// <summary>
        /// Inserts an entity into the database.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>True if the entity is successfully inserted; otherwise, false.</returns>
        bool Insert(T entity);
    }
}
