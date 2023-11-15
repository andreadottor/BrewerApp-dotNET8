namespace Dottor.BrewerApp.Common.Utilities;

using Dottor.BrewerApp.Common.Dtos;
using Dottor.BrewerApp.Common.Models;

internal class BeerMapper
{
    public static Beer Map(BeerDto dto)
    {
        var beer = new Beer
        {
            Id          = dto.Id,
            Name        = dto.Name,
            Tagline     = dto.Tagline,
            Description = dto.Description,
            FirstBrewed = dto.First_brewed,
            ImageUrl    = dto.Image_url,
            Abv         = dto.Abv,
            FoodPairing = dto.Food_pairing,
            BrewersTips = dto.Brewers_tips
        };

        return beer;
    }
}
