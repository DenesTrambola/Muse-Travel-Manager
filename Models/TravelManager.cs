using System.Text;

namespace Muse_Travel_Manager.Models;

public static class TravelManager
{
    public static List<Tour> Tours { get; private set; }

    public static List<Client> Clients { get; private set; }

    public static List<Booking> Bookings { get; private set; }

    public static void AddTour(Tour tour) =>
        Tours.Add(tour);

    public static void AddClient(Client client) =>
        Clients.Add(client);

    public static void CreateBooking(Tour tour, Client client) =>
        Bookings.Add(new(tour.TourId, client.ClientId));

    public static string GenerateReport()
    {
        var report = new StringBuilder();

        report.AppendLine("Tours:");
        foreach (var tour in Tours)
        {
            report.AppendLine(tour.GetTourInfo());
        }

        report.AppendLine("Clients:");
        foreach (var client in Clients)
        {
            report.AppendLine(client.GetFullName());
        }

        report.AppendLine("Bookings:");
        foreach (var booking in Bookings)
        {
            report.AppendLine($"Booking ID: {booking.BookingId}");
            report.AppendLine($"Tour ID: {booking.TourId}");
            report.AppendLine($"Client ID: {booking.ClientId}");
            report.AppendLine($"Booking Date: {booking.BookingDate}");
            report.AppendLine($"Status: {booking.Status}");
        }

        return report.ToString();
    }
}
