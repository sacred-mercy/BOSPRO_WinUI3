using BOSPRO.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;

namespace BOSPRO.Views;

public sealed partial class UserProgramCoursesPage : Page
{
    public static string program;
    public UserProgramCoursesViewModel ViewModel
    {
        get;
    }

    public UserProgramCoursesPage()
    {
        ViewModel = App.GetService<UserProgramCoursesViewModel>();
        InitializeComponent();
    }

    private void getCourseNames()
    {
        try
        {
            var connectionString = "Server=localhost;Database=bospro;Uid=root;Pwd=;";
            var conn = new MySqlConnection(connectionString);

            var sqlQuery = "SELECT `course_name` FROM `course` WHERE course_program_name=" + program + ";";

            var query = new MySqlCommand(sqlQuery, conn);
            conn.Open();
            var reader = query.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("test");
                Console.WriteLine(reader.GetString("course_name"));
                CourseSelectComboBox.Items.Add(reader.GetString("course_name"));
            }
            conn.Close();

        }
        catch (Exception)
        {
        }
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        program = (string)e.Parameter;
        Program_Title.Text = program;
        base.OnNavigatedTo(e);
        getCourseNames();
    }


    private void BackButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Frame.Navigate(typeof(UserHomePage));
    }

    private void EditButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

    }

    private void ViewButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

    }

    private void CoursePrintButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

    }

    private void ProgramPrintButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

    }
}
