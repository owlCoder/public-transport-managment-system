namespace Service.Database.CRUD
{
    /// <summary>
    /// Interface for database operations to delete entities of type T.
    /// </summary>
    /// <typeparam name="T">The type of entity to delete.</typeparam>
    public interface IDeleteOperation<T> where T : class
    {
        /// <summary>
        /// Deletes the specified entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>True if the entity is successfully deleted; otherwise, false.</returns>
        bool Delete(T entity);

        /// <summary>
        /// Deletes the entity with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>True if the entity with the specified ID is successfully deleted; otherwise, false.</returns>
        bool Delete(int id);
    }
}
