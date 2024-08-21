namespace ForeignExchangeRate.Helper.DBHelper
{
    public interface IDBHelper
    {
        Task<List<TResult>> ExecuteSP<TRequest, TResult>(string spName, TRequest request) where TRequest : class;
    }
}
