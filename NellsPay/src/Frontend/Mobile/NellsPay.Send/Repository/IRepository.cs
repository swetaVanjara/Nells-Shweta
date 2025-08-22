using System;
namespace NellsPay.Send.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Insert(TEntity entity);
        Task InsertAll(IEnumerable<TEntity> entities);
        Task Delete(TEntity entityToDelete);
        Task DeleteAll();
        Task<IList<TEntity>> GetAll();
        Task Update(TEntity entityToUpdate);
    }
}