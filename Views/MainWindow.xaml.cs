using Microsoft.Win32;
using Muse_Travel_Manager.Models;
using System.IO;
using System.Windows;

namespace Muse_Travel_Manager.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void OpenAddClientWindow(object sender, RoutedEventArgs e)
    {
        AddClientWindow addClientWindow = new AddClientWindow();
        if (addClientWindow.ShowDialog() is not null)
        {
            ClientsListBox.ItemsSource = null;
            ClientsListBox.ItemsSource = await TravelManager.GetClientsAsync();
        }
    }

    private async void OpenAddTourWindow(object sender, RoutedEventArgs e)
    {
        AddTourWindow addTourWindow = new AddTourWindow();
        if (addTourWindow.ShowDialog() is not null)
        {
            ToursDataGrid.ItemsSource = null;
            ToursDataGrid.ItemsSource = await TravelManager.GetToursAsync();
        }
    }

    private async void OpenBookingWindow(object sender, RoutedEventArgs e)
    {
        BookingWindow addBookingWindow = new BookingWindow();
        if (addBookingWindow.ShowDialog() is not null)
        {
            BookingsListBox.ItemsSource = null;
            BookingsListBox.ItemsSource = await TravelManager.GetBookingsAsync();
        }
    }

    private async void OpenAddDestinationWindow(object sender, RoutedEventArgs e)
    {
        AddDestinationWindow addDestinationWindow = new AddDestinationWindow();
        if (addDestinationWindow.ShowDialog() is not null)
        {
            DestinationsListBox.ItemsSource = null;
            DestinationsListBox.ItemsSource = await TravelManager.GetDestinationsAsync();
        }
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

    private async void LoadData(object sender, RoutedEventArgs e)
    {
        ToursDataGrid.ItemsSource = await TravelManager.GetToursAsync();
        DestinationsListBox.ItemsSource = await TravelManager.GetDestinationsAsync();
        ClientsListBox.ItemsSource = await TravelManager.GetClientsAsync();
        BookingsListBox.ItemsSource = await TravelManager.GetBookingsAsync();
    }
}
