using System;
using System.Data.Common;
using System.Reflection.PortableExecutable;
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
        delPrograms();
    }

    private void programAddbtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        addPrograms();
    }
    private void courseRemovebtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // TODO: Delete selected course of a program from db
    }

    private void AddCourseBtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // TODO: Add course to DB
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
        try
        {
            //This is my connection string i have assigned the database file address path
            var MyConnection2 = "Server=localhost;Database=bospro;Uid=root;Pwd=;";

            //Taking user-input from the text fields
            var pcode = programCode.Text;
            var pname = programName.Text;
            var pcollege = programCollege.Text;

            //This is the insert query in which we're taking input from the user 
            var Query = "INSERT INTO `programs`(`program_code`, `program_name`, `program_college`) VALUES('"+ pcode + "', '" + pname + "', '"+ pcollege +"');";

            //This is  MySqlConnection here we'll create the object and pass my connection string.
            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);

            //This is command class which will handle the query and connection object.
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.
            await databaseOkDialog.ShowAsync();
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

    private async void getProgramCode()
    { 
        try
        {
            var connectionString = "Server=localhost;Database=bospro;Uid=root;Pwd=;";
            var conn = new MySqlConnection(connectionString);
            var sqlQuery = "SELECT `program_code` FROM `programs`;";
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
    private async void delPrograms()
    {
        //string pcode;

        //try
        //{
        //    var connectionString = "Server=localhost;Database=bospro;Uid=root;Pwd=;";
        //    var conn = new MySqlConnection(connectionString);
        //    var sqlQuery = "SELECT `program_code` FROM `programs` WHERE 'program_name'='{}';";



        //    var query = new MySqlCommand(sqlQuery, conn);
        //    conn.Open();
        //    var reader = query.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        pcode = reader.GetString("program_code");
        //    }
        //    conn.Close();

        //}
        //catch (Exception)
        //{
        //    await databaseErrorDialog.ShowAsync();
        //}










        try
        {
            //This is my connection string i have assigned the database file address path
            var MyConnection2 = "Server=localhost;Database=bospro;Uid=root;Pwd=;";

            //This is the insert query in which we're taking input from the user 
            var Query = "DELETE FROM `programs` WHERE 'program_code'='aaa';"; 

            //This is  MySqlConnection here we'll create the object and pass my connection string.
            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);

            //This is command class which will handle the query and connection object.
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.
            await databaseOkDialog.ShowAsync();
            while (MyReader2.Read())
            {

            }
            MyConn2.Close();

            //string Query = "delete from student.studentinfo where idStudentInfo='" + this.IdTextBox.Text + "';";
            //MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
            //MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);

            //MySqlDataReader MyReader2;

            //MyConn2.Open();
            //MyReader2 = MyCommand2.ExecuteReader();
            //MessageBox.Show("Data Deleted");
            //while (MyReader2.Read())
            //{
            //}
            //MyConn2.Close();

        }
        catch (Exception)
        {
            await databaseErrorDialog.ShowAsync();
        }
    }
}