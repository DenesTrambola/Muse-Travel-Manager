using Microsoft.Win32;
using Muse_Travel_Manager.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Muse_Travel_Manager.Views;

public partial class HomePage : Page, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private IEnumerable<Tour> _tours;
    private IEnumerable<Client> _clients;
    private IEnumerable<Destination> _destinations;
    private IEnumerable<Booking> _bookings;

    private int _currentToursPage = 1;
    private int _currentClientsPage = 1;
    private int _currentDestinationsPage = 1;
    private int _currentBookingsPage = 1;

    private int _itemsPerPage = 10;

    private ObservableCollection<Tour> _pagedTours = new();
    private ObservableCollection<Client> _pagedClients = new();
    private ObservableCollection<Destination> _pagedDestinations = new();
    private ObservableCollection<Booking> _pagedBookings = new();

    public IEnumerable<Tour> PagedTours => _pagedTours;
    public IEnumerable<Client> PagedClients => _pagedClients;
    public IEnumerable<Destination> PagedDestinations => _pagedDestinations;
    public IEnumerable<Booking> PagedBookings => _pagedBookings;

    public HomePage()
    {
        InitializeComponent();
        DataContext = this;
    }

    private async void OpenAddClientWindow(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        mainWindow.MainFrame.Navigate(new AddClientPage());
    }

    private async void OpenAddTourWindow(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        mainWindow.MainFrame.Navigate(new AddTourPage());
    }

    private async void OpenBookingWindow(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        mainWindow.MainFrame.Navigate(new BookingPage());
    }

    private async void OpenAddDestinationWindow(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        mainWindow.MainFrame.Navigate(new AddDestinationPage());
    }

    private void GenerateReport(object sender, RoutedEventArgs e)
    {
        string report = TravelManager.GenerateReport();

        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        saveFileDialog.DefaultExt = ".txt";

        if (saveFileDialog.ShowDialog() == true)
        {
            string filePath = saveFileDialog.FileName;
            File.WriteAllText(filePath, report);

            MessageBox.Show("Звіт успішно збережено!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private void ShowTours(object sender, RoutedEventArgs e)
    {
        ToursSection.Visibility = Visibility.Visible;
        ClientsSection.Visibility = Visibility.Collapsed;
        DestinationsSection.Visibility = Visibility.Collapsed;
        BookingsSection.Visibility = Visibility.Collapsed;

        SidebarShowToursBtn.IsEnabled = false;
        SidebarShowClientsBtn.IsEnabled = true;
        SidebarShowDestinationsBtn.IsEnabled = true;
        SidebarShowBookingsBtn.IsEnabled = true;

        BottombarShowToursBtn.IsEnabled = false;
        BottombarShowClientsBtn.IsEnabled = true;
        BottombarShowDestinationsBtn.IsEnabled = true;
        BottombarShowBookingsBtn.IsEnabled = true;
    }

    private void ShowClients(object sender, RoutedEventArgs e)
    {
        ToursSection.Visibility = Visibility.Collapsed;
        ClientsSection.Visibility = Visibility.Visible;
        DestinationsSection.Visibility = Visibility.Collapsed;
        BookingsSection.Visibility = Visibility.Collapsed;

        SidebarShowToursBtn.IsEnabled = true;
        SidebarShowClientsBtn.IsEnabled = false;
        SidebarShowDestinationsBtn.IsEnabled = true;
        SidebarShowBookingsBtn.IsEnabled = true;

        BottombarShowToursBtn.IsEnabled = true;
        BottombarShowClientsBtn.IsEnabled = false;
        BottombarShowDestinationsBtn.IsEnabled = true;
        BottombarShowBookingsBtn.IsEnabled = true;
    }

    private void ShowDestinations(object sender, RoutedEventArgs e)
    {
        ToursSection.Visibility = Visibility.Collapsed;
        ClientsSection.Visibility = Visibility.Collapsed;
        DestinationsSection.Visibility = Visibility.Visible;
        BookingsSection.Visibility = Visibility.Collapsed;

        SidebarShowToursBtn.IsEnabled = true;
        SidebarShowClientsBtn.IsEnabled = true;
        SidebarShowDestinationsBtn.IsEnabled = false;
        SidebarShowBookingsBtn.IsEnabled = true;

        BottombarShowToursBtn.IsEnabled = true;
        BottombarShowClientsBtn.IsEnabled = true;
        BottombarShowDestinationsBtn.IsEnabled = false;
        BottombarShowBookingsBtn.IsEnabled = true;
    }

    private void ShowBookings(object sender, RoutedEventArgs e)
    {
        ToursSection.Visibility = Visibility.Collapsed;
        ClientsSection.Visibility = Visibility.Collapsed;
        DestinationsSection.Visibility = Visibility.Collapsed;
        BookingsSection.Visibility = Visibility.Visible;

        SidebarShowToursBtn.IsEnabled = true;
        SidebarShowClientsBtn.IsEnabled = true;
        SidebarShowDestinationsBtn.IsEnabled = true;
        SidebarShowBookingsBtn.IsEnabled = false;

        BottombarShowToursBtn.IsEnabled = true;
        BottombarShowClientsBtn.IsEnabled = true;
        BottombarShowDestinationsBtn.IsEnabled = true;
        BottombarShowBookingsBtn.IsEnabled = false;
    }

    private async void LoadData(object sender, RoutedEventArgs e)
    {
        _tours = await TravelManager.GetToursAsync();
        _destinations = await TravelManager.GetDestinationsAsync();
        _clients = await TravelManager.GetClientsAsync();
        _bookings = await TravelManager.GetBookingsAsync();

        if (_tours.Count() == 0)
        {
            EmptyToursText.Visibility = Visibility.Visible;
            ToursDataGrid.Visibility = Visibility.Collapsed;
            ToursPagination.Visibility = Visibility.Collapsed;
        }
        else
        {
            EmptyToursText.Visibility = Visibility.Collapsed;
            ToursDataGrid.Visibility = Visibility.Visible;
            ToursPagination.Visibility = Visibility.Visible;
        }

        if (_destinations.Count() == 0)
        {
            EmptyDestinationsText.Visibility = Visibility.Visible;
            DestinationsDataGrid.Visibility = Visibility.Collapsed;
            DestinationsPagination.Visibility = Visibility.Collapsed;
        }
        else
        {
            EmptyDestinationsText.Visibility = Visibility.Collapsed;
            DestinationsDataGrid.Visibility = Visibility.Visible;
            DestinationsPagination.Visibility = Visibility.Visible;
        }

        if (_clients.Count() == 0)
        {
            EmptyClientsText.Visibility = Visibility.Visible;
            ClientsDataGrid.Visibility = Visibility.Collapsed;
            ClientsPagination.Visibility = Visibility.Collapsed;
        }
        else
        {
            EmptyClientsText.Visibility = Visibility.Collapsed;
            ClientsDataGrid.Visibility = Visibility.Visible;
            ClientsPagination.Visibility = Visibility.Visible;
        }

        if (_bookings.Count() == 0)
        {
            EmptyBookingsText.Visibility = Visibility.Visible;
            BookingsDataGrid.Visibility = Visibility.Collapsed;
            BookingsPagination.Visibility = Visibility.Collapsed;
        }
        else
        {
            EmptyBookingsText.Visibility = Visibility.Collapsed;
            BookingsDataGrid.Visibility = Visibility.Visible;
            BookingsPagination.Visibility = Visibility.Visible;
        }

        UpdateToursPagination();
        UpdateClientsPagination();
        UpdateDestinationsPagination();
        UpdateBookingsPagination();
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (sender is Page page)
        {
            if (page.ActualWidth < 806)
            {
                Sidebar.Visibility = Visibility.Collapsed;
                BottomBar.Visibility = Visibility.Visible;
            }
            else
            {
                Sidebar.Visibility = Visibility.Visible;
                BottomBar.Visibility = Visibility.Collapsed;
            }
        }
    }

    private void UpdateToursPagination()
    {
        var pagedList = _tours.Skip((_currentToursPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
        _pagedTours.Clear();
        foreach (var tour in pagedList)
            _pagedTours.Add(tour);

        ToursPageIndicator.Text = $"Page {_currentToursPage} of {Math.Ceiling((double)_tours.Count() / _itemsPerPage)}";
    }

    private void UpdateClientsPagination()
    {
        var pagedList = _clients.Skip((_currentClientsPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
        _pagedClients.Clear();
        foreach (var client in pagedList)
            _pagedClients.Add(client);
        ClientsPageIndicator.Text = $"Page {_currentClientsPage} of {Math.Ceiling((double)_clients.Count() / _itemsPerPage)}";
    }

    private void UpdateDestinationsPagination()
    {
        var pagedList = _destinations.Skip((_currentDestinationsPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
        _pagedDestinations.Clear();
        foreach (var destination in pagedList)
            _pagedDestinations.Add(destination);

        DestinationsPageIndicator.Text = $"Page {_currentDestinationsPage} of {Math.Ceiling((double)_destinations.Count() / _itemsPerPage)}";
    }

    private void UpdateBookingsPagination()
    {
        var pagedList = _bookings.Skip((_currentBookingsPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
        _pagedBookings.Clear();
        foreach (var booking in pagedList)
            _pagedBookings.Add(booking);

        BookingsPageIndicator.Text = $"Page {_currentBookingsPage} of {Math.Ceiling((double)_bookings.Count() / _itemsPerPage)}";
    }

    private void NextToursPage_Click(object sender, RoutedEventArgs e)
    {
        if (_currentToursPage * _itemsPerPage < _tours.Count())
        {
            _currentToursPage++;
            UpdateToursPagination();
        }
    }

    private void PreviousToursPage_Click(object sender, RoutedEventArgs e)
    {
        if (_currentToursPage > 1)
        {
            _currentToursPage--;
            UpdateToursPagination();
        }
    }

    private void NextClientsPage_Click(object sender, RoutedEventArgs e)
    {
        if (_currentClientsPage * _itemsPerPage < _clients.Count())
        {
            _currentClientsPage++;
            UpdateClientsPagination();
        }
    }

    private void PreviousClientsPage_Click(object sender, RoutedEventArgs e)
    {
        if (_currentClientsPage > 1)
        {
            _currentClientsPage--;
            UpdateClientsPagination();
        }
    }

    private void NextDestinationsPage_Click(object sender, RoutedEventArgs e)
    {
        if (_currentDestinationsPage * _itemsPerPage < _destinations.Count())
        {
            _currentDestinationsPage++;
            UpdateDestinationsPagination();
        }
    }

    private void PreviousDestinationsPage_Click(object sender, RoutedEventArgs e)
    {
        if (_currentDestinationsPage > 1)
        {
            _currentDestinationsPage--;
            UpdateDestinationsPagination();
        }
    }

    private void NextBookingsPage_Click(object sender, RoutedEventArgs e)
    {
        if (_currentBookingsPage * _itemsPerPage < _bookings.Count())
        {
            _currentBookingsPage++;
            UpdateBookingsPagination();
        }
    }

    private void PreviousBookingsPage_Click(object sender, RoutedEventArgs e)
    {
        if (_currentBookingsPage > 1)
        {
            _currentBookingsPage--;
            UpdateBookingsPagination();
        }
    }
}
