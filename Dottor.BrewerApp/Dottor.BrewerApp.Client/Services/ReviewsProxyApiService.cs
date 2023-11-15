namespace Dottor.BrewerApp.Client.Services;

using Dottor.BrewerApp.Common.Models;
using System.Net.Http;
using System.Net.Http.Json;

public class ReviewsProxyApiService(HttpClient httpClient) : IReviewsProxyService
{
    public async Task<List<Review>> GetReviewsAsync(int beerId)
    {
        var response = await httpClient.GetAsync($"api/v1/beers/{beerId}/reviews");
        response.EnsureSuccessStatusCode();
        var list = await response.Content.ReadFromJsonAsync<List<Review>>();
        return list;
    }

    public async Task CreateReviewAsync(Review review)
    {
        var response = await httpClient.PostAsJsonAsync($"api/v1/beers/{review.BeerId}/reviews", review);
        response.EnsureSuccessStatusCode();
    }
}
