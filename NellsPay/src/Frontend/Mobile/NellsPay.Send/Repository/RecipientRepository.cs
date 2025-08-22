using NellsPay.Send.ResponseModels;
using SQLite;

namespace NellsPay.Send.Repository;

public class RecipientRepository : GenericRepository<Recipient>, IRecipientRepository
{
    public RecipientRepository(IDbContext context) : base(context)
    {
    }
    public async Task<List<Recipient>> GetFavoriteRecipientAsync()
    {
        try
        {
            var results = await GetAll();
            return results.Where(r => r.IsFavorite).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving favorite recipients.", ex);
        }
    }

    public async Task<Recipient> FavoriteRecipientAsync(Recipient recipient)
    {
        recipient.IsFavorite = true;
        await Insert(recipient);
        return recipient;
    }

    public async Task<Recipient> UnFavoriteRecipientAsync(Recipient recipient)
    {
        recipient.IsFavorite = false;
        await Delete(recipient);
        return recipient;
    }
}