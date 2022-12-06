using BOSPRO.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace BOSPRO.Views;

public sealed partial class UserEditCoursePage : Page
{
    public UserEditCourseViewModel ViewModel
    {
        get;
    }

    public UserEditCoursePage()
    {
        ViewModel = App.GetService<UserEditCourseViewModel>();
        InitializeComponent();
    }
}
