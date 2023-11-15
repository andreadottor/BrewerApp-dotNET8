namespace Dottor.BrewerApp.Common.Models
{
    public record Beer
    {

        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Tagline { get; init; } = default!;
        public string FirstBrewed { get; init; } = default!;
        public string Description { get; init; } = default!;
        public string ImageUrl { get; init; } = default!;
        /// <summary>
        /// Alcohol by Volume
        /// </summary>
        public float Abv { get; init; }

        public string[] FoodPairing { get; init; } = default!;
        public string BrewersTips { get; init; } = default!;
    }
}
