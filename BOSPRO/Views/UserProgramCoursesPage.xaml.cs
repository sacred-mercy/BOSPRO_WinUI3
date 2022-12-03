using BOSPRO.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BOSPRO.Views;

public sealed partial class UserProgramCoursesPage : Page
{
    public UserProgramCoursesViewModel ViewModel
    {
        get;
    }

    public UserProgramCoursesPage()
    {
        ViewModel = App.GetService<UserProgramCoursesViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        var program = (string)e.Parameter;
        Program_Title.Text = program;
        base.OnNavigatedTo(e);
    }


    private void BackButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Frame.Navigate(typeof(UserHomePage));
    }
}
