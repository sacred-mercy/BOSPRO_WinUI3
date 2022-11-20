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

    private void programAddbtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // TODO: Add the program to db
    }

    private void programCode_TextChanged(object sender, TextChangedEventArgs e)
    {
        programCodeStatusText.Text = string.Empty;
    }

    private void programName_TextChanged(object sender, TextChangedEventArgs e)
    {
        programNameStatusText.Text = string.Empty;
    }

    private void programCollege_TextChanged(object sender, TextChangedEventArgs e)
    {
        programCollegeStatusText.Text = string.Empty;
    }

    private void courseRemovebtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // TODO: Delete selected course of a program from db
    }
}
