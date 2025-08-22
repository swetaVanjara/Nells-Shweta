using NellsPay.Send.ResponseModels;
using SQLite;

namespace NellsPay.Send.Repository;

public class FxRepository : GenericRepository<FxConvert>, IFxRepository
{
    public FxRepository(IDbContext context) : base(context)
    {
    }

    public async Task<FxConvert> GetFxConvertsAsync()
    {
        try
        {
            var results = await GetAll();
            return results.FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving FX conversions.", ex);
        }
       
    }
}