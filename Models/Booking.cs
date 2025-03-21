namespace Muse_Travel_Manager.Models;

public enum BookingStatus
{
    Pending,
    Confirmed,
    Cancelled
}

public class Booking
{
    public int BookingId { get; private set; }

    public int TourId { get; private set; }

    public int ClientId { get; private set; }

    public DateTime BookingDate { get; private set; }

    public BookingStatus Status { get; private set; }

    public Tour Tour { get; private set; }

    public Client Client { get; private set; }

    public Booking(int tourId, int clientId)
    {
        TourId = tourId;
        ClientId = clientId;
        BookingDate = DateTime.Now;
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
