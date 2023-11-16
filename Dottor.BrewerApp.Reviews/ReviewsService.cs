namespace Dottor.BrewerApp.Reviews;

using Dottor.BrewerApp.Reviews.Models;
using LiteDB;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


internal class ReviewsService : IReviewsService, IDisposable
{
    private readonly LiteDatabase _db;

    public ReviewsService(IConfiguration configuration)
    {
        _db = new LiteDatabase(configuration.GetConnectionString("ReviewDatabase"));
    }

    public void Execute(CreateReview command)
    {
        var review = new Review
        {
            BeerId       = command.BeerId,
            Rating       = command.Rating,
            Text         = command.Text,
            CreationDate = DateTimeOffset.Now
        };
        _db.GetCollection<Review>("reviews")
           .Insert(review);
    }

    public IEnumerable<Review> Execute(GetReviews query)
    {
        return _db.GetCollection<Review>("reviews")
                  .Query()
                  .Where(x => x.BeerId == query.BeerId)
                  .OrderByDescending(x => x.CreationDate)
                  .ToList();
    }

    public void Dispose()
    {
        _db?.Dispose();
    }
}
