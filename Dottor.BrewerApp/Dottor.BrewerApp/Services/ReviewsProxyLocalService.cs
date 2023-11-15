namespace Dottor.BrewerApp.Services;

using Dottor.BrewerApp.Client.Services;
using Dottor.BrewerApp.Common.Models;
using Dottor.BrewerApp.Reviews;
using Reviews = Dottor.BrewerApp.Reviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ReviewsProxyLocalService(IReviewsService reviewsService) : IReviewsProxyService
{
    public Task CreateReviewAsync(Review review)
    {
        var command = new Reviews.CreateReview(review.BeerId, review.Rating, review.Text);
        reviewsService.Execute(command);
        return Task.CompletedTask;
    }

    public Task<List<Review>> GetReviewsAsync(int beerId)
    {
        var list = reviewsService.Execute(new Reviews.GetReviews(beerId));
        var results = list.Select(b => new Review
        {
            BeerId = b.BeerId,
            Rating = b.Rating,
            Text   = b.Text
        }).ToList();
        return Task.FromResult(results);
    }
}
