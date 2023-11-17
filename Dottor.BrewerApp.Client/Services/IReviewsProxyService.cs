namespace Dottor.BrewerApp.Client.Services;

using Dottor.BrewerApp.Common.Models;

public interface IReviewsProxyService
{
    Task<List<Review>> GetReviewsAsync(int beerId);

    Task CreateReviewAsync(Review review);
}
