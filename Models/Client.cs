namespace Muse_Travel_Manager.Models;

public class Client(string firstName, string lastName, string phone, string email)
{
    public int ClientId { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Phone { get; private set; }

    public string Email { get; private set; }

    public string GetFullName() => $"{FirstName} {LastName}";
}
