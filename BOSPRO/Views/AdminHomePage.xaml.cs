using BOSPRO.ViewModels;

using Microsoft.UI.Xaml.Controls;

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
    }
}
