namespace Muse_Travel_Manager.Models;

public class Tour(string destination, decimal price, DateTime startDate, int duration, string imagePath) : VisualEntity(imagePath)
{
    public int TourId { get; private set; }

    public string Destination { get; private set; }

    public decimal Price { get; private set; }

    public DateTime StartDate { get; private set; }

    public int Duration { get; private set; }

    public string ImagePath { get; private set; }

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
