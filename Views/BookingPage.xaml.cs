using Muse_Travel_Manager.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Muse_Travel_Manager.Views;

public partial class BookingPage : Page, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string _tourImage = string.Empty;

    public string TourImage
    {
        get => _tourImage;
        set
        {
            _tourImage = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TourImage)));
        }
    }

    private Tour? _selectedTour;

    public Tour? SelectedTour
    {
        get => _selectedTour;
        set
        {
            _selectedTour = value;
            TourImage = _selectedTour?.ImagePath ?? string.Empty;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTour)));
        }
    }

    public DateTime BookingDate { get; set; } = DateTime.UtcNow;

    public BookingPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    private async void SaveBooking(object sender, RoutedEventArgs e)
    {
        if (TourComboBox.SelectedItem is null || ClientComboBox.SelectedItem is null)
        {
            MessageBox.Show("Будь ласка, заповніть правильно всі поля!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        int tourId = SelectedTour!.TourId;
        int clientId = (await TravelManager.GetClientsAsync()).First(c => c.GetFullName() == ClientComboBox.SelectedItem.ToString()?.Split(',')[0]).ClientId;

        Tour? tour = await TravelManager.GetTourByIdAsync(tourId);
        Client? client = await TravelManager.GetClientByIdAsync(clientId);

        if (tour is null || client is null)
        {
            MessageBox.Show("Не знайдено такого туру або клієнта!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        await TravelManager.CreateBookingAsync(tour, client);

        MessageBox.Show("Бронювання підтверджено!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
        Cancel(sender, e);
    }

    private void Cancel(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        mainWindow.MainFrame.Navigate(new HomePage());
    }

    private async void LoadData(object sender, RoutedEventArgs e)
    {
        IEnumerable<Tour> tours = await TravelManager.GetToursAsync();
        IEnumerable<Client> clients = await TravelManager.GetClientsAsync();

        TourComboBox.ItemsSource = tours;
        ClientComboBox.ItemsSource = clients.Select(c => $"{c.GetFullName()}, {c.Email}, {c.Phone}");
    }
}
