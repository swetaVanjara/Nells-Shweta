using NellsPay.Send.ResponseModels;
using SQLite;

namespace NellsPay.Send.Repository;

public interface IRecipientRepository : IRepository<Recipient>
{
    Task<Recipient> FavoriteRecipientAsync(Recipient recipient);
    Task<Recipient> UnFavoriteRecipientAsync(Recipient recipient);
    Task<List<Recipient>> GetFavoriteRecipientAsync();
}