using System.Windows;

namespace Muse_Travel_Manager.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new HomePage());
    }
}
