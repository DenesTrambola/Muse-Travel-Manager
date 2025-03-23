using Muse_Travel_Manager.Models;
using System.Windows;

namespace Muse_Travel_Manager.Views;

public partial class AddClientWindow : Window
{
    public AddClientWindow()
    {
        InitializeComponent();
    }

    private async void OnSaveClick(object sender, RoutedEventArgs e)
    {
        string firstName = FirstNameTextBox.Text;
        string lastName = LastNameTextBox.Text;
        string phone = PhoneTextBox.Text;
        string email = EmailTextBox.Text;

        if (string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(phone) ||
            string.IsNullOrWhiteSpace(email))
        {
            MessageBox.Show("Будь ласка, заповніть правильно всі поля!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        try
        {
            Client client = new Client(firstName, lastName, phone, email);
            await TravelManager.AddClientAsync(client);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBox.Show("Клієнт доданий успішно!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
        this.Close();
    }

    private void Cancel(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
