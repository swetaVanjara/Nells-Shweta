using NellsPay.Send.Repository;
using NellsPay.Send.ResponseModels;
using SQLite;
using SQLitePCL;

namespace NellsPay.Send.Repository;

public sealed class MobileDbContext : IDbContext
{
    private readonly string _databasePath =
        Path.Combine(FileSystem.AppDataDirectory, "NellsPaySQLite.db3");

    private Lazy<SQLiteAsyncConnection> _connHolder;
    private readonly Task _initTask;

    public MobileDbContext()
    {
        Batteries_V2.Init();

        _connHolder = new Lazy<SQLiteAsyncConnection>(() =>
            new SQLiteAsyncConnection(_databasePath,
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache));

        // kick off initialization ONCE, but as a Task we can await later
        _initTask = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        var db = _connHolder.Value;

        // create schema here
        await db.CreateTableAsync<Recipient>();
        await db.CreateTableAsync<FxConvert>();
        await db.CreateTableAsync<Data>();
    }

    public async Task<SQLiteAsyncConnection> GetDatabaseConnectionAsync()
    {
        await _initTask;              
        return _connHolder.Value;
    }

    public async Task DropAllTablesAsync()
    {
        var db = await GetDatabaseConnectionAsync();
        await db.DropTableAsync<Recipient>();
        await db.DropTableAsync<FxConvert>();
        await db.DropTableAsync<Data>();
    }
}