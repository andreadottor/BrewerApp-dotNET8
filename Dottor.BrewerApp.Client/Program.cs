using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Dottor.BrewerApp.Common;
using Dottor.BrewerApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBrewerServices();
builder.Services.AddScoped<IReviewsProxyService, ReviewsProxyApiService>();

await builder.Build().RunAsync();
