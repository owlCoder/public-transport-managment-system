namespace Service.Database.CRUD
{
    /// <summary>
    /// Interface for database operations to update entities of type T.
    /// </summary>
    /// <typeparam name="T">The type of entity to update.</typeparam>
    public interface IUpdateOperation<T> where T : class
    {
        /// <summary>
        /// Updates the entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the entity to update.</param>
        /// <param name="entity">The updated entity data.</param>
        /// <returns>True if the entity is successfully updated; otherwise, false.</returns>
        bool Update(int id, T entity);
    }
}
