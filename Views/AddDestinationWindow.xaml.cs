using Microsoft.Win32;
using Muse_Travel_Manager.Models;
using System.ComponentModel;
using System.Windows;

namespace Muse_Travel_Manager.Views;

public partial class AddDestinationWindow : Window, INotifyPropertyChanged
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

    public AddDestinationWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void SelectImage(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "Image Files|*.jpg;*.png;*.jpeg",
            Title = "Виберіть зображення для напрямку"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            ImagePath = openFileDialog.FileName;
        }
    }

    private async void SaveDestination(object sender, RoutedEventArgs e)
    {
        string name = DestinationNameTextBox.Text;
        string description = DestinationDescriptionTextBox.Text;
        string popularSeason = SeasonComboBox.Text;

        if (string.IsNullOrEmpty(name))
        {
            MessageBox.Show("Будь ласка, заповніть правильно всі поля!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        try
        {
            Destination destination = new Destination(name, description, popularSeason, ImagePath == String.Empty ? null : ImagePath);
            await TravelManager.AddDestinationAsync(destination);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBox.Show("Напрямок додано успішно!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
        this.Close();
    }

    private void Cancel(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
