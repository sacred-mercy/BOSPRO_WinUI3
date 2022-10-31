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
        ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        //AdminName.Content = localSettings.Values["name"] as string;
    }
}
