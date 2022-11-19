using BOSPRO.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;

namespace BOSPRO.Views;

public sealed partial class AdminHomePage : Page
{
    public AdminHomeViewModel ViewModel
    {
        get;
    }

    public AdminHomePage()
    {
        ViewModel = App.GetService<AdminHomeViewModel>();
        InitializeComponent();

        // To fetch Admin name
        _ = Windows.Storage.ApplicationData.Current.LocalSettings;
        //AdminName.Content = localSettings.Values["name"] as string;

        // TODO: Add Programs Name from DB to ProgramRemoveComboBox for reference see UserHomePage.xaml.cs
    }

    private void programRemovebtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // TODO: Delete selected program from Database
    }
}
