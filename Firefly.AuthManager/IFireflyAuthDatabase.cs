using System.Data;

namespace Firefly.AuthManager
{
    internal interface IFireflyAuthDatabase
    {
        IDbConnection GetConnection();
    }
}