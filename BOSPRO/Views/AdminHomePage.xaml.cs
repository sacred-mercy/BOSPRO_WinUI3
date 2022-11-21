using System;
using System.Data.Common;
using System.Xml.Linq;
using BOSPRO.ViewModels;
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using Windows.Storage;

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

        // To fetch Admin name
        _ = Windows.Storage.ApplicationData.Current.LocalSettings;
        //AdminName.Content = localSettings.Values["name"] as string;

        // TODO: Add Programs Name from DB to ProgramRemoveComboBox for reference see UserHomePage.xaml.cs
        getProgramNamesFromDatabase();
    }

    private void programRemovebtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // TODO: Delete selected program from Database
    }

    private void programAddbtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        addPrograms();
    }

    private void programCode_TextChanged(object sender, TextChangedEventArgs e)
    {
        programCodeStatusText.Text = string.Empty;
    }

    private void programName_TextChanged(object sender, TextChangedEventArgs e)
    {
        programNameStatusText.Text = string.Empty;
    }

    private void programCollege_TextChanged(object sender, TextChangedEventArgs e)
    {
        programCollegeStatusText.Text = string.Empty;
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
                ProgramRemoveComboBox.Items.Add(reader.GetString("program_name"));
            }
            conn.Close();

        }
        catch (Exception)
        {
            await databaseErrorDialog.ShowAsync();
        }
    }

    private async void addPrograms()
    {
        //var connectionString = "Server=localhost;Database=bospro;Uid=root;Pwd=;";
        try
        {
            //This is my connection string i have assigned the database file address path
            //string MyConnection2 = "datasource=localhost;port=3307;username=root;password=root";
            var MyConnection2 = "Server=localhost;Database=bospro;Uid=root;Pwd=;";

            var pcode = programCode.Text;
            var pname = programName.Text;
            var pcollege = programCollege.Text;

            //This is my insert query in which i am taking input from the user through windows forms
            var Query = "INSERT INTO `programs`(`program_code`, `program_name`, `program_college`) VALUES('"+ pcode + "', '" + pname + "', '"+ pcollege +"');";

            //This is  MySqlConnection here i have created the object and pass my connection string.
            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);

            //This is command class which will handle the query and connection object.
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.
            //MessageBox.Show("Save Data");
            while (MyReader2.Read())
            {

            }
            MyConn2.Close();

        }
        catch (Exception)
        {
            await databaseErrorDialog.ShowAsync();
        }
    }



}

//var sql = "INSERT INTO 'programs'(program_code, program_name, program_college) VALUES(aaa, aaa, aaa)";