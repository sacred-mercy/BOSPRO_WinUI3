using BOSPRO.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage;
using Microsoft.VisualBasic;
//using static Google.Protobuf.Collections.MapField<TKey, TValue>;
using System.Security.AccessControl;
using Org.BouncyCastle.Utilities;

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

    private async void SubmitButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

        //Taking user-input from the text fields
        var obj = courseObjective.Text;
        var syll = courseSyllabus.Text;
        var outc = courseOutcome.Text;
        var refe = courseReference.Text;

        //connection string assigned the database file address path
        var MyConnection2 = "Server=localhost;Database=bospro;Uid=root;Pwd=;";


        var conn = new MySqlConnection(MyConnection2);

        var sqlQuery = "SELECT `outcome` FROM `course_data` WHERE code=\"" + courseCode + "\";";

        var query = new MySqlCommand(sqlQuery, conn);
        conn.Open();
        //var reader = query.ExecuteReader();

        var result = query.ExecuteScalar();

        if (result != outc && result is not null)
        {
            //courseObjective.Text = "Record exists";

            string query2 = "UPDATE course_data " +
    "SET objective=@obj, syllabus=@syll, outcome=@outc, reference=@refe " +
    " WHERE code=@code;";

            using (MySqlCommand command = new MySqlCommand(query2, conn))
            {
                //updating data in the sql table with the initial variables  
                command.Parameters.AddWithValue("@code", courseCode);
                command.Parameters.AddWithValue("@obj", obj);
                command.Parameters.AddWithValue("@syll", syll);
                command.Parameters.AddWithValue("@outc", outc);
                command.Parameters.AddWithValue("@refe", refe);

                command.ExecuteNonQuery();

            }

        }
        else
        {

            //This is the insert query in which we're taking input from the user 
            var Query = "INSERT INTO `course_data`(`code`, " +
                "`objective`, `syllabus`, `outcome`, `reference`)" +
                " VALUES('" + courseCode + "', '" + obj + "', '" + syll + "', '" + outc + "'" +
                ", '" + refe + "');";

            //This is  MySqlConnection here we'll create the object and pass my connection string.
            var MyConn2 = new MySqlConnection(MyConnection2);

            //This is command class which will handle the query and connection object.
            var MyCommand2 = new MySqlCommand(Query, MyConn2);
            MyConn2.Open();
            _ = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.

            conn.Close();
        }
        
        //##################  Agar textboxes clear karna ho to ye un-comment kardena ##################

        //Emptying the text fields
        //courseObjective.Text = "";
        //courseSyllabus.Text = "";
        //courseOutcome.Text = "";
        //courseReference.Text = "";

    }

    private async void OpenButton_Click(object sender, RoutedEventArgs e)
    {
        // Open a text file.
        var open =
            new FileOpenPicker
            {
                SuggestedStartLocation =
            PickerLocationId.DocumentsLibrary
            };
        open.FileTypeFilter.Add(".rtf");

        StorageFile file = await open.PickSingleFileAsync();

        if (file != null)
        {
            using var randAccStream =
                await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            // Load the file into the Document property of the RichEditBox.
            //editor.Document.LoadFromStream(Microsoft.UI.Text.TextSetOptions.FormatRtf, randAccStream);
        }
    }
}
