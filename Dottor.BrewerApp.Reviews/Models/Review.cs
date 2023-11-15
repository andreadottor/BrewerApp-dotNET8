namespace Dottor.BrewerApp.Reviews.Models;

using System;

public class Review
{
    public int Id { get; set; }
    public int BeerId { get; set; }
    public double Rating { get; set; }
    public string? Text { get; set; }
    public DateTimeOffset CreationDate { get; set; }
}