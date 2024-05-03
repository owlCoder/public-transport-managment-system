namespace Service.Database.Operations
{
    /// <summary>
    /// Defines the contract for create (insert) operations on entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of entity to perform the create operation on.</typeparam>
    public interface ICreateOperation<T> where T : class
    {
        /// <summary>
        /// Inserts the specified entity into the database.
        /// </summary>
        /// <param name="entity">The entity to be inserted.</param>
        /// <returns>
        /// <c>true</c> if the insert operation was successful; otherwise, <c>false</c>.
        /// </returns>
        bool Insert(T entity);
    }
}