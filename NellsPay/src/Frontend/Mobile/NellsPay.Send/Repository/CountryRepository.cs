using NellsPay.Send.ResponseModels;
using SQLite;

namespace NellsPay.Send.Repository;

public class CountryRepository : GenericRepository<Data>, ICountryRepository
{
    public CountryRepository(IDbContext context) : base(context)
    {
    }

    public async Task<List<Data>> GetAllCountries()
    {
        try
        {
            var results = await GetAll();
            return results.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving countries.", ex);
        }
       
    }

    public async Task<Data> ToggleFavCountry(Data country)
    {
         try
        {
            await Update(country);
            return country;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving countries.", ex);
        }
    }

    public async Task<Data> ToggleFavCurrency(Data currency)
    {
         try
        {
            await Update(currency);
            return currency;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving countries.", ex);
        }
    }
}