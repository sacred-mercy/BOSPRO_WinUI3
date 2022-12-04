using BOSPRO.ViewModels;
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;

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

        //To insert years in year ComboBox
        for (var i = 2020; i <= 2030; i++)
        {
            YearComboBox.Items.Add(i);
        }

        /* To insert semesters in semester ComboBox
        for (var i = 1; i <= 10; i++)
        {
            SemesterComboBox.Items.Add(i);
        } */

        // Get Program names
        getProgramNamesFromDatabase();

    }

    private async void getProgramNamesFromDatabase()
    {
        var connectionString = "Server=localhost;Database=bospro;Uid=root;Pwd=;";
        try
        {
            var conn = new MySqlConnection(connectionString);
            var sqlQuery = "SELECT `program_name` FROM `programs`;";
            var query = new MySqlCommand(sqlQuery, conn);
            conn.Open();
            var reader = query.ExecuteReader();
            while (reader.Read())
            {
                ProgramComboBox.Items.Add(reader.GetString("program_name"));
            }
            conn.Close();

        }
        catch (Exception)
        {
            await databaseErrorDialog.ShowAsync();
        }
    }

    private void GoBtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Frame.Navigate(typeof(UserProgramCoursesPage), ProgramComboBox.SelectedItem);
    }

    private void BackButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Frame.Navigate(typeof(MainPage));
    }
}
