using BOSPRO.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;

namespace BOSPRO.Views;

public sealed partial class UserEditCoursePage : Page
{
    public string program = "";
    public string course = "";
    public string year = "";
    public string courseCode = "";

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
        year = arr[1];
        course = arr[2];
        Course_Title.Text = course;
        GetCourseCode();
        base.OnNavigatedTo(e);
    }

    private void GetCourseCode()
    {
        var connectionString = "Server=localhost;Database=bospro;Uid=root;Pwd=;";
        var conn = new MySqlConnection(connectionString);

        var sqlQuery = "SELECT `course_code` FROM `course` WHERE course_program_name=\"" + program + "\" AND course_name=\"" + course + "\";";

        var query = new MySqlCommand(sqlQuery, conn);
        conn.Open();
        var reader = query.ExecuteReader();

        while (reader.Read())
        {
            courseCode = reader.GetString("course_code");
        }
        conn.Close();
    }

    private void BackButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        string[] arr = { program, year };
        Frame.Navigate(typeof(UserProgramCoursesPage), arr);
    }
}
