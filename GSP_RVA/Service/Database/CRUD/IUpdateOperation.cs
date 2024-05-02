namespace Service.Database.CRUD
{
    public interface IUpdateOperation<T> where T : class
    {
        bool Update(int id, T entity);
    }
}
