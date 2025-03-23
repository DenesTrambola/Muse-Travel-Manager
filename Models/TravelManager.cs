using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Muse_Travel_Manager.Models;

public static class TravelManager
{
    private static List<Tour> Tours { get; set; } = new();
    private static List<Client> Clients { get; set; } = new();
    private static List<Booking> Bookings { get; set; } = new();
    private static List<Destination> Destinations { get; set; } = new();

    private static async Task LoadDataAsync()
    {
        using (var db = new ApplicationDbContext())
        {
            Tours = await db.Tours.ToListAsync();
            Clients = await db.Clients.ToListAsync();
            Bookings = await db.Bookings.ToListAsync();
            Destinations = await db.Destinations.ToListAsync();
        }
    }

    public static async Task<IEnumerable<Tour>> GetToursAsync()
    {
        await LoadDataAsync();
        return Tours ?? Enumerable.Empty<Tour>();
    }

    public static async Task<IEnumerable<Client>> GetClientsAsync()
    {
        await LoadDataAsync();
        return Clients ?? Enumerable.Empty<Client>();
    }

    public static async Task<IEnumerable<Booking>> GetBookingsAsync()
    {
        await LoadDataAsync();
        return Bookings ?? Enumerable.Empty<Booking>();
    }

    public static async Task<IEnumerable<Destination>> GetDestinationsAsync()
    {
        await LoadDataAsync();
        return Destinations ?? Enumerable.Empty<Destination>();
    }

    public static async Task AddTourAsync(Tour tour)
    {
        await LoadDataAsync();

        using (var db = new ApplicationDbContext())
        {
            db.Tours.Add(tour);
            await db.SaveChangesAsync();
        }
        Tours.Add(tour);
    }

    public static async Task<Tour?> GetTourByIdAsync(int tourId)
    {
        await LoadDataAsync();
        return Tours.FirstOrDefault(t => t.TourId == tourId);
    }

    public static async Task<Client?> GetClientByIdAsync(int clientId)
    {
        await LoadDataAsync();
        return Clients.FirstOrDefault(c => c.ClientId == clientId);
    }

    public static async Task<Booking?> GetBookingByIdAsync(int bookingId)
    {
        await LoadDataAsync();
        return Bookings.FirstOrDefault(b => b.BookingId == bookingId);
    }

    public static async Task<Destination?> GetDestinationByIdAsync(int destinationId)
    {
        await LoadDataAsync();
        return Destinations.FirstOrDefault(d => d.DestinationId == destinationId);
    }

    public static async Task AddClientAsync(Client client)
    {
        await LoadDataAsync();

        if (Clients.Any(c => c.Phone == client.Phone))
            throw new InvalidOperationException("Клієнт з таким номером телефону вже існує!");
        else if (Clients.Any(c => c.Email == client.Email))
            throw new InvalidOperationException("Клієнт з такою електронною адресою вже існує!");

        using (var db = new ApplicationDbContext())
        {
            db.Clients.Add(client);
            await db.SaveChangesAsync();
        }
        Clients.Add(client);
    }

    public static async Task AddDestinationAsync(Destination destination)
    {
        await LoadDataAsync();

        if (Destinations.Any(d => d.Name == destination.Name))
            throw new InvalidOperationException("Місце призначення з такою назвою вже існує!");

        using (var db = new ApplicationDbContext())
        {
            db.Destinations.Add(destination);
            await db.SaveChangesAsync();
        }
        Destinations.Add(destination);
    }

    public static async Task CreateBookingAsync(Tour tour, Client client)
    {
        await LoadDataAsync();

        var booking = new Booking(tour.TourId, client.ClientId);
        booking.ConfirmBooking();
        using (var db = new ApplicationDbContext())
        {
            db.Bookings.Add(booking);
            await db.SaveChangesAsync();
        }
        Bookings.Add(booking);
    }

    public static string GenerateReport()
    {
        var report = new StringBuilder();

        report.AppendLine("📌 Тури:");
        foreach (var tour in Tours)
            report.AppendLine(tour.GetTourInfo() + "\n");

        report.AppendLine("\n📌 Клієнти:");
        foreach (var client in Clients)
            report.AppendLine(client.GetClientInfo() + "\n");

        report.AppendLine("\n📌 Бронювання:");
        foreach (var booking in Bookings)
            report.AppendLine(booking.GetBookingInfo() + "\n");

        report.AppendLine("\n📌 Напрямки:");
        foreach (var destination in Destinations)
            report.AppendLine(destination.GetDestinationDetails() + "\n");

        return report.ToString();
    }
}
