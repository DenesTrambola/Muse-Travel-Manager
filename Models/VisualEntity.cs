namespace Muse_Travel_Manager.Models;

public abstract class VisualEntity
{
    public string ImagePath { get; private set; }

    public VisualEntity(string imagePath)
    {
        ImagePath = imagePath;
    }

    public virtual void LoadImage()
    {
        Console.WriteLine($"Displaying image from path: {ImagePath}");
    }
}
