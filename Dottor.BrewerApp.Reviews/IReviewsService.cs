namespace Dottor.BrewerApp.Reviews
{
    using Dottor.BrewerApp.Reviews.Models;
    using System.Collections.Generic;

    public interface IReviewsService
    {
        void Execute(CreateReview command);
        IEnumerable<Review> Execute(GetReviews query);
    }
}