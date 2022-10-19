using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static Dapper.SqlMapper;

namespace Juguetes.Persistence.Dapper
{
    public class Dapper_ORM : IDapper_ORM
    {
        public string ConnectionString { get; set; }

        public Dapper_ORM(IConfiguration config)
        {
            if (ConnectionString == null)
            {
                ConnectionString = config.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(ConnectionString));
            }
        }

        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<T>(sp, parms, commandTimeout: 120, commandType: commandType).FirstOrDefault();
            }
        }

        public IList<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<T>(sp, parms, commandTimeout: 120, commandType: commandType).ToList();
            }
        }

        public void Dispose()
        {
        }
    }
}