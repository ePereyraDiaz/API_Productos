using System.Data;
using Dapper;

namespace Juguetes.Persistence.Dapper
{
    public interface IDapper_ORM : IDisposable
	{
        T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        IList<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

    }
}
