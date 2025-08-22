
using SQLite;

namespace NellsPay.Send.Repository;

public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
{
       private readonly IDbContext _ctx;

       protected GenericRepository(IDbContext ctx) => _ctx = ctx;

       private Task<SQLiteAsyncConnection> Conn() => _ctx.GetDatabaseConnectionAsync();

       public async Task Insert(TEntity entity)
              => await (await Conn()).InsertOrReplaceAsync(entity);

       public async Task InsertAll(IEnumerable<TEntity> entities)
              => await (await Conn()).InsertAllAsync(entities);

       public async Task Delete(TEntity entityToDelete)
              => await (await Conn()).DeleteAsync(entityToDelete);

       public async Task DeleteAll()
           => await (await Conn()).DeleteAllAsync<TEntity>();

       public async Task<IList<TEntity>> GetAll()
          => await (await Conn()).Table<TEntity>().ToListAsync();
          
       public async Task Update(TEntity entity)
            => await (await Conn()).UpdateAsync(entity);

}