using SQLite;

namespace NellsPay.Send.Repository
{
    public interface IDbContext
    {
        Task<SQLiteAsyncConnection> GetDatabaseConnectionAsync();
    }
}