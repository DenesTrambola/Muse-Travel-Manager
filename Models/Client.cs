using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
    public string Phone { get; private set; } = null!;

    [Required]
    public string Email { get; private set; } = null!;

    private Client() { }

    public Client(string firstName, string lastName, string phone, string email)
    {
        string phonePattern = @"^\+?3?8?(0\d{9})$";
        if (!Regex.IsMatch(phone, phonePattern))
            throw new Exception("Формат номера телефону невірний!");

        string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        if (!Regex.IsMatch(email, emailPattern))
            throw new Exception("Формат електронної адреси невірний!");

        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
    }

    public string GetFullName() => $"{FirstName} {LastName}";

    public string GetClientInfo()
    {
        return $"Ім'я: {FirstName}\n" +
               $"Прізвище: {LastName}\n" +
               $"Телефон: {Phone}\n" +
               $"Email: {Email}";
    }
}
