using BOSPRO.ViewModels;
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;

namespace BOSPRO.Views;

public sealed partial class UserHomePage : Page
{
    static int yearNum = 0;
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
        var program = (string)ProgramComboBox.SelectedItem;
        var year = yearNum.ToString();
        if (year == "0") year = "";
        if (string.IsNullOrEmpty(program) || string.IsNullOrEmpty(year))
        {
            if (string.IsNullOrEmpty(program))
            {
                programStatusText.Text = "Select a program";
            }
            if (string.IsNullOrEmpty(year))
            {
                yearStatusText.Text = "Select a year";
            }
            return;
        }
        string[] arr = { program, year };
        Frame.Navigate(typeof(UserProgramCoursesPage), arr);
    }

    private void BackButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Frame.Navigate(typeof(MainPage));
    }

    private void ProgramComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        programStatusText.Text = "";
    }

    private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        yearNum = (int)YearComboBox.SelectedItem;
        yearStatusText.Text = "";
    }
}
