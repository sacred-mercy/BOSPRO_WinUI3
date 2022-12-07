using BOSPRO.ViewModels;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BOSPRO.Views;

public sealed partial class UserEditCoursePage : Page
{
    public string program = "";
    public string course = "";
    public UserEditCourseViewModel ViewModel
    {
        get;
    }

    public UserEditCoursePage()
    {
        ViewModel = App.GetService<UserEditCourseViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        var arr = (string[])e.Parameter;
        program = arr[0];
        course = arr[1];
        Course_Title.Text = course;
        base.OnNavigatedTo(e);
    }

    private void BackButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Frame.Navigate(typeof(UserProgramCoursesPage));
    }
}
