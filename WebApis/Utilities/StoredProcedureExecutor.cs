using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace WebApis.Utilities
{
    public class StoredProcedureExecutor
    {
        private readonly IDbConnection _connection;

        public StoredProcedureExecutor(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task ExecuteProcedureAsync(string procedureName, object parameters)
        {
            await _connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
        }
    }

}
