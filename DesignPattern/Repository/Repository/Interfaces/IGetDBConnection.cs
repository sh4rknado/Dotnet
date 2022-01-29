using Repository.Model;
using System.Data;

namespace Repository.Interfaces
{
    public interface IGetDBConnection
    {
        bool TryGetDBConnection(out IDbConnection dbConnection);
    }
}
