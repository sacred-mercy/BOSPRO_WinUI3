using BOSPRO.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace BOSPRO.Views;

public sealed partial class UserHomePage : Page
{
    public UserHomeViewModel ViewModel
    {
        get;
    }

    public UserHomePage()
    {
        ViewModel = App.GetService<UserHomeViewModel>();
        InitializeComponent();
    }
}
