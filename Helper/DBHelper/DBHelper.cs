using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ForeignExchangeRate.Helper.DBHelper
{
    public class DBHelper : IDBHelper
    {
        private readonly string _connectionString = "Data Source=CHAN;Initial Catalog=ForeignExchangeRate;Integrated Security=true;";
        public DBHelper()
        { }

        public async Task<List<TResult>> ExecuteSP<TRequest, TResult>(string spName, TRequest request) where TRequest : class
        {
            List<TResult> response = new List<TResult>();

            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                GetDynamicParameters(ref dynamicParameters, ref request);                
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    IEnumerable<TResult> entities = await sqlConnection.QueryAsync<TResult>(spName, dynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: 30);
                    response = entities.ToList();
                }
            }
            catch
            {
                throw;
            }

            return response;
        }

        private void GetDynamicParameters<T>(ref DynamicParameters dynamicParameters, ref T input) where T : class
        {
            foreach (PropertyInfo propertyInfo in input.GetType().GetProperties())
            {
                DBParameterAttribute dBParameterAttribute = (DBParameterAttribute)propertyInfo.GetCustomAttributes(typeof(DBParameterAttribute), true).SingleOrDefault();

                if (dBParameterAttribute != null)
                {
                    object value = propertyInfo.GetValue(input, null) ?? DBNull.Value;
                    dynamicParameters.Add(propertyInfo.Name, value, dBParameterAttribute.Type);
                }
            }
        }
    }

    public class DBParameterAttribute : Attribute
    {
        public DbType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
        private DbType _type = DbType.String;
    }
}
