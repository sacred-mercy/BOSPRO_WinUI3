using BOSPRO.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using Windows.Storage;

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
            errorBar.Title = "Empty E-Mail or Password";
            return;
        }
        var verificationEmail = "";
        var verificationPassword = "";

        var connectionString = "Server=localhost;Database=bospro;Uid=root;Pwd=;";
        try
        {
            var conn = new MySqlConnection(connectionString);
            var sqlQuery = "SELECT * FROM `users` WHERE email='" + email + "';";
            var query = new MySqlCommand(sqlQuery, conn);
            conn.Open();
            var reader = query.ExecuteReader();
            while (reader.Read())
            {
                verificationEmail = reader.GetString("email");
                verificationPassword = reader.GetString("password");

                //store user session in localdata
                ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["name"] = reader.GetString("name");
                localSettings.Values["role"] = reader.GetString("role");
                localSettings.Values["college"] = reader.GetString("college");
            }
            conn.Close();

            if (email.Equals(verificationEmail) && password.Equals(verificationPassword))
            {
                ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                var role = localSettings.Values["role"] as string;
                if (role.Equals("admin"))
                {
                    Frame.Navigate(typeof(AdminHomePage), null);

                }
                else
                {
                    Frame.Navigate(typeof(UserHomePage), null);
                }
            }
            else
            {
                errorBar.IsOpen = true;
                errorBar.Title = "Incorrect Credentials";
                errorBar.Message = "Please enter vaild E-Mail or Password";
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
