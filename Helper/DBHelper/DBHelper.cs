using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ForeignExchangeRate.Helper.DBHelper
{
    /// <summary>
    /// DBHelper
    /// </summary>
    public class DBHelper : IDBHelper
    {
        private readonly string _connectionString = "Data Source=CHAN;Initial Catalog=ForeignExchangeRate;Integrated Security=true;";

        public DBHelper()
        {
        }

        /// <summary>
        /// 執行SP，並將結果轉換成指定的型別。
        /// </summary>
        /// <typeparam name="TRequest">參數的型別</typeparam>
        /// <typeparam name="TResult">結果的型別</typeparam>
        /// <param name="spName">要執行的SP名稱</param>
        /// <param name="request">請求參數</param>
        /// <returns>執行結果</returns>
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

        /// <summary>
        /// 根據物件屬性，自動將其轉換成 DynamicParameters 參數
        /// </summary>
        /// <typeparam name="T">物件的型別</typeparam>
        /// <param name="dynamicParameters">DynamicParameters 參數物件</param>
        /// <param name="input">要轉換的物件</param>
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

    /// <summary>
    /// 資料庫參數的型別屬性
    /// </summary>
    public class DBParameterAttribute : Attribute
    {
        /// <summary>
        /// 資料庫參數的型別
        /// </summary>
        public DbType Type { get; set; } = DbType.String;
    }
}