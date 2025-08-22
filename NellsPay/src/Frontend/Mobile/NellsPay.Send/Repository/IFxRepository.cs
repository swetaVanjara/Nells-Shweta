using NellsPay.Send.ResponseModels;
using SQLite;

namespace NellsPay.Send.Repository;

public interface IFxRepository : IRepository<FxConvert>
{
    Task<FxConvert> GetFxConvertsAsync();
}