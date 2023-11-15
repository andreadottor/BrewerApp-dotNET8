namespace Dottor.BrewerApp.Reviews;


using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddReviewsServices(this IServiceCollection services)
    {
    
        services.AddScoped<IReviewsService, ReviewsService>();

    }
}