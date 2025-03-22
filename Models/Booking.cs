using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Muse_Travel_Manager.Models;

public enum BookingStatus
{
    Pending,
    Confirmed,
    Cancelled
}

public class Booking
{
    [Key]
    public int BookingId { get; private set; }

    [Required]
    public int TourId { get; private set; }

    [Required]
    public int ClientId { get; private set; }

    [Required]
    public DateTime BookingDate { get; private set; }

    [Required]
    public BookingStatus Status { get; private set; }

    [ForeignKey(nameof(TourId))]
    public Tour Tour { get; private set; } = null!;

    [ForeignKey(nameof(ClientId))]
    public Client Client { get; private set; } = null!;

    private Booking() { }

    public Booking(int tourId, int clientId)
    {
        TourId = tourId;
        ClientId = clientId;
        BookingDate = DateTime.UtcNow;
        Status = BookingStatus.Pending;
    }

    public void ConfirmBooking()
    {
        Status = BookingStatus.Confirmed;
    }

    public void CancelBooking()
    {
        Status = BookingStatus.Cancelled;
    }
}
