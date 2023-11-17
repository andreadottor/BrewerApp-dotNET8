namespace Dottor.BrewerApp.Endpoints;

using Dottor.BrewerApp.Reviews;
using Dottor.BrewerApp.Reviews.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

public static class ReviewsEndpoints
{
    public static IEndpointRouteBuilder MapReviewsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/v1/beers/{beerId}/reviews");
        group.WithTags("Public");
        group.WithOpenApi();

        group.MapGet("", GetReviewAsync)
             .WithName("GetReviewAsync")
             .WithSummary("Get beers review's")
             .WithDescription("Return reviews of the specified beer");

        group.MapPost("", CreateReviewAsync)
            .WithName("CreateReviewAsync")
             .WithSummary("Create beer review")
             .WithDescription("Create a new review for the specified beer");

        return endpoints;
    }

    private static Results<Ok<IEnumerable<Review>>, ProblemHttpResult> GetReviewAsync(int beerId, IReviewsService service, ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger(nameof(ReviewsEndpoints));
        try
        {
            var query = new GetReviews(beerId);
            var list = service.Execute(query);
            return TypedResults.Ok(list);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error on retrieve beers review.");
            return TypedResults.Problem(ex.Message, title: "Error on Reviews API");
        }
    }

    private static Results<NoContent, BadRequest<string>, ProblemHttpResult> CreateReviewAsync(int beerId, CreateReview command, IReviewsService service, ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger(nameof(ReviewsEndpoints));
        try
        {
            if (beerId != command.BeerId)
                return TypedResults.BadRequest("BeerId in path and in body are different.");

            service.Execute(command);
            return TypedResults.NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error on insert new review.");
            return TypedResults.Problem(ex.Message, title: "Error on Reviews API");
        }
    }

}