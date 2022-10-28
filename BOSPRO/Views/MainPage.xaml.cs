using BOSPRO.ViewModels;
using MySql.Data.MySqlClient;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace BOSPRO.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }

    private void Login_btn_pressed(object sender, RoutedEventArgs e)
    {
        var email = emailText.Text;
        var password = passwordText.Password;
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            errorBar.IsOpen = true;
            errorBar.Title = "Please Enter E-Mail And Password";
            return;
        }
        var verificationEmail = ""; 
        var verificationPassword = "";
        var id = 0;
        var name = "";
        var role = "";
        var college = "";

        var connectionString = "Server=localhost;Database=bospro;Uid=root;Pwd=;";
        try
        {
            var conn = new MySqlConnection(connectionString);
            var sqlQuery = "select * from users;";
            var query = new MySqlCommand(sqlQuery, conn);
            conn.Open();
            var reader = query.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader.GetInt64("id");
                verificationEmail = reader.GetString("email");
                verificationPassword = reader.GetString("password");
                name = reader.GetString("name");
                role = reader.GetString("role");
                college = reader.GetString("college");
            }
            conn.Close();

            if (email.Equals(verificationEmail) && password.Equals(verificationPassword))
            {
                if (role.Equals("admin"))
                {
                    Frame.Navigate(typeof(AdminHomePage), null);

                } else
                {
                    Frame.Navigate(typeof(UserHomePage), null);
                }
            }
            else
            {
                errorBar.IsOpen = true;
                errorBar.Title = "Incorrect Credentials";
                errorBar.Message = "Please enter correct E-Mail or Password";
            }
        }
        catch (Exception)
        {
            errorBar.IsOpen = true;
            errorBar.Title = "Connection error";
            errorBar.Message = "Can't connect to database. Try later or contact admin";
        }
        
    }
}
