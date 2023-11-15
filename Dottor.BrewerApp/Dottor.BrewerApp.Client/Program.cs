using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Dottor.BrewerApp.Common;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBrewerServices();

await builder.Build().RunAsync();
