using System.Reflection.PortableExecutable;
using BOSPRO.ViewModels;
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using Windows.Storage;

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

        //To insert semesters in semester ComboBox
        for (var i = 1; i <= 10; i++)
        {
            SemesterComboBox.Items.Add(i);
        }

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
            
        }
    }
}
