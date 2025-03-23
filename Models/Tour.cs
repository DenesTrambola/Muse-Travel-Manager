using System.ComponentModel.DataAnnotations;

namespace Muse_Travel_Manager.Models;

public class Tour : VisualEntity
{
    [Key]
    public int TourId { get; private set; }

    [Required]
    public string Destination { get; private set; } = null!;

    [Required]
    public decimal Price { get; private set; }

    [Required]
    public DateTime StartDate { get; private set; }

    [Required]
    public int Duration { get; private set; }

    private Tour() { }

    public Tour(string destination, decimal price, DateTime startDate, int duration, string? imagePath)
        : base(imagePath)
    {
        Destination = destination;
        Price = price;
        StartDate = startDate;
        Duration = duration;
    }

    public string GetTourInfo() =>
        $"Tour ID: {TourId}\n" +
        $"Destination: {Destination}\n" +
        $"Price: {Price}\n" +
        $"Start Date: {StartDate}\n" +
        $"Duration: {Duration}\n" +
        $"Image Path: {ImagePath}";

    public override void LoadImage()
    {
        base.LoadImage();
    }
}
