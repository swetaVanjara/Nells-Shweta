using NellsPay.Send.ResponseModels;
using SQLite;

namespace NellsPay.Send.Repository;

public interface ICountryRepository : IRepository<Data>
{
    Task<List<Data>> GetAllCountries();
    Task<Data> ToggleFavCurrency(Data country);
    Task<Data> ToggleFavCountry(Data country);
}