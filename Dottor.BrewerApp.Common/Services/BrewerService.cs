namespace Dottor.BrewerApp.Common.Services;

using Dottor.BrewerApp.Common.Dtos;
using Dottor.BrewerApp.Common.Models;
using Dottor.BrewerApp.Common.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// API documentation https://punkapi.com/
/// </remarks>
internal class BrewerService(IHttpClientFactory _httpClientFactory, ILogger<BrewerService> _logger) : IBrewerService
{
    public const string BaseUrl = "https://api.punkapi.com";
    public const string HttpClientName = "Brewer";

    public async Task<IEnumerable<Beer>> GetBeersAsync()
    {
        await Task.Delay(2000);

        return await GetBeersAsync(null);
    }

    public async Task<IEnumerable<Beer>> GetBeersAsync(string? filterText)
    {
        var client = _httpClientFactory.CreateClient(HttpClientName);
        var httpRespose = await client.GetAsync("v2/beers");

        if (httpRespose.IsSuccessStatusCode)
        {
            var list = await httpRespose.Content.ReadFromJsonAsync<BeerDto[]>();
            if (list is null)
                return Enumerable.Empty<Beer>();

            var beers = list.Select(x => BeerMapper.Map(x));

            if (!string.IsNullOrWhiteSpace(filterText))
                beers = beers.Where(x => x.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase));

            return beers;
        }
        else
        {
            _logger.LogError("Error on call 'v2/beers/'. Response status code: {StatusCode} {ReasonPhrase}", (int)httpRespose.StatusCode, httpRespose.ReasonPhrase);
        }

        return Enumerable.Empty<Beer>();
    }


    public async Task<Beer?> GetBeerAsync(int id)
    {
        var client = _httpClientFactory.CreateClient(HttpClientName);
        var httpRespose = await client.GetAsync($"v2/beers/{id}");

        if (httpRespose.IsSuccessStatusCode)
        {
            var list = await httpRespose.Content.ReadFromJsonAsync<BeerDto[]>();
            if (list is not null && list.Length > 0)
            {
                var beer = BeerMapper.Map(list[0]);
                return beer;
            }
        }
        else
        {
            _logger.LogError("Error on call 'v2/beers/{id}'. Response status code: {StatusCode} {ReasonPhrase}", id, (int)httpRespose.StatusCode, httpRespose.ReasonPhrase);
        }

        return null;
    }

    public async Task<Beer> GetRandomBeerAsync()
    {
        var client = _httpClientFactory.CreateClient(HttpClientName);
        var httpRespose = await client.GetAsync("v2/beers/random");

        if (httpRespose.IsSuccessStatusCode)
        {
            var list = await httpRespose.Content.ReadFromJsonAsync<BeerDto[]>();
            if (list is not null &&
                list.Length > 0)
            {
                var beer = BeerMapper.Map(list[0]);
                return beer;
            }
        }
        else
        {
            _logger.LogError("Error on call 'v2/beers/random'. Response status code: {StatusCode} {ReasonPhrase}", (int)httpRespose.StatusCode, httpRespose.ReasonPhrase);
        }

        throw new Exception("Random beer API return no beer.");
    }


}
