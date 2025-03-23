using Microsoft.Win32;
using Muse_Travel_Manager.Models;
using System.ComponentModel;
using System.Windows;

namespace Muse_Travel_Manager.Views;

public partial class AddTourWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string _imagePath = String.Empty;

    public string ImagePath
    {
        get => _imagePath;
        set
        {
            _imagePath = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImagePath)));
        }
    }

    public AddTourWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void SelectImage(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "Image Files|*.jpg;*.png;*.jpeg",
            Title = "Виберіть зображення для туру"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            ImagePath = openFileDialog.FileName;
        }
    }

    private async void SaveTour(object sender, RoutedEventArgs e)
    {
        string destination = DestinationComboBox.Text;
        decimal price = -1;
        DateTime startDate;
        int duration;

        if (string.IsNullOrWhiteSpace(destination) ||
            string.IsNullOrWhiteSpace(TourPriceTextBox.Text) ||
            string.IsNullOrWhiteSpace(TourDatePicker.Text) ||
            string.IsNullOrWhiteSpace(DurationTextBox.Text))
        {
            MessageBox.Show("Будь ласка, заповніть правильно всі поля!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (!decimal.TryParse(TourPriceTextBox.Text, out price))
        {
            MessageBox.Show("Ціна туру повинна бути числом!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        else if (!DateTime.TryParse(TourDatePicker.Text, out startDate))
        {
            MessageBox.Show("Дата початку туру введена некоректно!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        else if (!int.TryParse(DurationTextBox.Text, out duration))
        {
            MessageBox.Show("Тривалість туру повинна бути числом!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Tour tour = new Tour(destination, price, startDate, duration, ImagePath == String.Empty ? null : ImagePath);
        await TravelManager.AddTourAsync(tour);

        MessageBox.Show("Тур додано успішно!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
        this.Close();
    }

    private void Cancel(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private async void LoadData(object sender, RoutedEventArgs e)
    {
        IEnumerable<Destination> destinations = await TravelManager.GetDestinationsAsync();

        DestinationComboBox.ItemsSource = destinations.Select(d => d.Name);
    }
}
