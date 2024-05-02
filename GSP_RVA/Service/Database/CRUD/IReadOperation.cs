namespace Service.Database.Operations
{
    public interface IReadOperation<T> where T : class
    {
        T Read(int id);
    }
}
