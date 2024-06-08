namespace Service.Database.CRUD
{
    public interface IDeleteOperation<T> where T : class
    {
        bool Delete(T entity);

        bool Delete(int id);
    }
}