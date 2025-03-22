using System.ComponentModel.DataAnnotations;

namespace Muse_Travel_Manager.Models;

public class Client
{
    [Key]
    public int ClientId { get; private set; }

    [Required]
    public string FirstName { get; private set; } = null!;

    [Required]
    public string LastName { get; private set; } = null!;

    [Required]
    [Phone]
    public string Phone { get; private set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; private set; } = null!;

    private Client() { }

    public Client(string firstName, string lastName, string phone, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
    }

    public string GetFullName() => $"{FirstName} {LastName}";
}
