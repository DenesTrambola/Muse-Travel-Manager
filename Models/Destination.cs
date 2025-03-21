namespace Muse_Travel_Manager.Models;

public class Destination(int destinationId, string name, string description, string popularSeason, string imagePath) : VisualEntity(imagePath)
{
    public int DestinationId { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public string PopularSeason { get; private set; }

    public string ImagePath { get; private set; }

    public string GetDestinationDetails() =>
        $"Destination: {Name}\n" +
        $"Description: {Description}\n" +
        $"Popular Season: {PopularSeason}\n" +
        $"Image Path: {ImagePath}";

    public override void LoadImage()
    {
        base.LoadImage();
    }
}
