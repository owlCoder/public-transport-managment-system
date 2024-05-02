namespace Service.Database.Operations
{
    public interface ICreateOperation<T> where T : class
    {
        bool Insert(T entity);
    }
}
