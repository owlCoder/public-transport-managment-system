namespace Service.Database.CRUD
{
    /// <summary>
    /// Defines the contract for update operations on entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of entity to perform the update operation on.</typeparam>
    public interface IUpdateOperation<T> where T : class
    {
        /// <summary>
        /// Updates the entity with the specified identifier in the database with the provided entity.
        /// </summary>
        /// <param name="id">The identifier of the entity to update.</param>
        /// <param name="entity">The updated entity to replace the existing one.</param>
        /// <returns>
        /// <c>true</c> if the update operation was successful; otherwise, <c>false</c>.
        /// </returns>
        bool Update(int id, T entity);
    }
}