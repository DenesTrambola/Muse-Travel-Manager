using System.ComponentModel.DataAnnotations;

namespace Muse_Travel_Manager.Models;

public class Destination : VisualEntity
{
    [Key]
    public int DestinationId { get; private set; }

    [Required]
    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public string? PopularSeason { get; private set; }

    public Destination() { }

    public Destination(string name, string? description, string? popularSeason, string? imagePath)
        : base(imagePath)
    {
        Name = name;
        Description = description;
        PopularSeason = popularSeason;
    }

    public string GetDestinationDetails() =>
        $"Destination: {Name}\n" +
        $"Description: {Description}\n" +
        $"Popular Season: {PopularSeason}\n" +
        $"Image Path: {ImagePath}";
}
