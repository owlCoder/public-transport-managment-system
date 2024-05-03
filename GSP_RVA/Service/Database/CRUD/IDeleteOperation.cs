namespace Service.Database.CRUD
{
    /// <summary>
    /// Defines the contract for delete operations on entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of entity to perform the delete operation on.</typeparam>
    public interface IDeleteOperation<T> where T : class
    {
        /// <summary>
        /// Deletes the specified entity from the database.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <returns>
        /// <c>true</c> if the delete operation was successful; otherwise, <c>false</c>.
        /// </returns>
        bool Delete(T entity);

        /// <summary>
        /// Deletes the entity with the specified identifier from the database.
        /// </summary>
        /// <param name="id">The identifier of the entity to be deleted.</param>
        /// <returns>
        /// <c>true</c> if the delete operation was successful; otherwise, <c>false</c>.
        /// </returns>
        bool Delete(int id);
    }
}