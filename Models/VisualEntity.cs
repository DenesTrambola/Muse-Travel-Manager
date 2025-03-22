namespace Muse_Travel_Manager.Models;

public abstract class VisualEntity
{
    public string? ImagePath { get; protected set; }

    protected VisualEntity() { }

    protected VisualEntity(string? imagePath)
    {
        ImagePath = imagePath;
    }

    public virtual void LoadImage()
    {
        if (ImagePath is null)
        {
            Console.WriteLine("No image path provided!");
            return;
        }

        if (ImagePath.EndsWith(".png"))
            Console.WriteLine($"Displaying PNG image: {ImagePath}");
        else if (ImagePath.EndsWith(".jpg") || ImagePath.EndsWith(".jpeg"))
            Console.WriteLine($"Displaying JPEG image: {ImagePath}");
        else
            Console.WriteLine($"Unknown format: {ImagePath}");
    }
}
