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

    private async void Login_btn_pressed(object sender, RoutedEventArgs e)
    {
        var email = emailText.Text;
        var password = passwordText.Password;

        //verify that input box are not empty
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            if (string.IsNullOrEmpty(email))
            {
                emailStatusText.Text = "Email Field is required";
            }
            if (string.IsNullOrEmpty(password))
            {
                passwordStatusText.Text = "Password Field is required";
            }
            return;
        }

        var connectionString = "Server=localhost;Database=bospro;Uid=root;Pwd=;";
        try
        {
            //local storage
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var conn = new MySqlConnection(connectionString);
            var sqlQuery = "SELECT * FROM `users` WHERE email='" + email + "';";
            var query = new MySqlCommand(sqlQuery, conn);
            conn.Open();
            var reader = query.ExecuteReader();
            while (reader.Read())
            {
                //store user session in localdata
                localSettings.Values["name"] = reader.GetString("name");
                localSettings.Values["email"] = reader.GetString("email");
                localSettings.Values["password"] = reader.GetString("password");
                localSettings.Values["role"] = reader.GetString("role");
                localSettings.Values["college"] = reader.GetString("college");
            }
            conn.Close();
            var dbEmail = localSettings.Values["email"] as string;
            var dbPassword = localSettings.Values["password"] as string;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (dbEmail.Equals(email) && dbPassword.Equals(password))
            {
                //checking role 
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
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            else
            {
                emailAndPasswordStatusText.Text = "Invalid Email or Password";
                passwordText.Password = "";
            }
        }
        catch (Exception)
        {
            await databaseErrorDialog.ShowAsync();
        }
    }

    private void emailText_TextChanged(object sender, TextChangedEventArgs e)
    {
        emailStatusText.Text = string.Empty;
    }

    private void passwordText_PasswordChanged(object sender, RoutedEventArgs e)
    {
        passwordStatusText.Text = string.Empty;
    }
}
