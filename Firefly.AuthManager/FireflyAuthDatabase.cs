using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Firefly.AuthManager
{
    internal class FireflyAuthDatabase : IFireflyAuthDatabase
    {
        private readonly string connectionString;

        public FireflyAuthDatabase(IOptions<DatabaseSettings> options)
        {
            connectionString = options?.Value?.ConnectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<T> Execute<T>(Func<IDbConnection, Task<T>> func)
        {
            T result = default(T);
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                result = await func.Invoke(connection);
            }

            return result;
        }

        public IDbConnection GetConnection()
        {
            var connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
