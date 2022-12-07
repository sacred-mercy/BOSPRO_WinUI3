using BOSPRO.ViewModels;
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;

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
        var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        TabbedPivot_Profile.Header = localSettings.Values["name"] as string;

        //Add Program Names from DB to ProgramRemoveComboBox
        getProgramNamesFromDatabase();
    }

    private void getCoursesFromDatabase()
    {
        var connectionString = "Server=localhost;Database=bospro;Uid=root;Pwd=;";
        var conn = new MySqlConnection(connectionString);
        var program = programCourseRemoveComboBox.SelectedItem;
        var sqlQuery = "SELECT `course_name` FROM `course` WHERE course_program_name=\"" + program + "\";";

        var query = new MySqlCommand(sqlQuery, conn);
        conn.Open();
        var reader = query.ExecuteReader();

        while (reader.Read())
        {
            courseRemoveComboBox.Items.Add(reader.GetString("course_name"));
        }
        conn.Close();
    }

    private void programRemovebtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        //Deletes program from DB
        delPrograms();
    }

    private void programAddbtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        //Adds program to the DB
        addPrograms();
    }

    private void courseRemovebtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var course = courseRemoveComboBox.SelectedItem;
        var program = programCourseRemoveComboBox.SelectedItem;
        //This is my connection string i have assigned the database file address path
        var connectionString = "Server=localhost;Database=bospro;Uid=root;Pwd=;";
        var conn = new MySqlConnection(connectionString);
        //This is the insert query in which we're taking input from the user 
        var Query = "DELETE FROM `course` WHERE course_program_name='" + program +
            "' AND course_name='" + course + "';";

        //This is  MySqlConnection here we'll create the object and pass my connection string.
        var MyConn2 = new MySqlConnection(connectionString);

        //This is command class which will handle the query and connection object.
        var MyCommand2 = new MySqlCommand(Query, MyConn2);
        MySqlDataReader MyReader2;
        MyConn2.Open();
        MyReader2 = MyCommand2.ExecuteReader();
        // Here our query will be executed and data saved into the database.
        while (MyReader2.Read())
        {

        }
        MyConn2.Close();
        courseRemoveComboBox.Items.Clear();
        courseRemoveComboBox.IsEnabled = false;
    }

    private void AddCourseBtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        //Taking user-input from the text fields

        var program = courseAddProgramNameComboBox.SelectedItem;
        var code = courseCodeToAddCourse.Text;
        var sem = courseSemToAddCourse.Text;
        var credit = courseCreditToAddCourse.Text;
        var ltp = courseLTPToAddCourse.Text;
        var name = courseNameToAddCourse.Text;

        //connection string assigned the database file address path
        var MyConnection2 = "Server=localhost;Database=bospro;Uid=root;Pwd=;";

        //This is the insert query in which we're taking input from the user 
        var Query = "INSERT INTO `course`(`course_code`, `course_name`, " +
            "`course_credit`, `course_ltp`, `course_semester`, `course_program_name`)" +
            " VALUES('" + code + "', '" + name + "', '" + credit + "', '" + ltp + "'" +
            ", '" + sem + "', '" + program + "');";

        //This is  MySqlConnection here we'll create the object and pass my connection string.
        var MyConn2 = new MySqlConnection(MyConnection2);

        //This is command class which will handle the query and connection object.
        var MyCommand2 = new MySqlCommand(Query, MyConn2);
        MyConn2.Open();
        _ = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.

        //Emptying the text fields
        courseAddProgramNameComboBox.SelectedItem = "";
        courseCodeToAddCourse.Text = "";
        courseSemToAddCourse.Text = "";
        courseCreditToAddCourse.Text = "";
        courseLTPToAddCourse.Text = "";
        courseNameToAddCourse.Text = "";
    }

    private async void getProgramNamesFromDatabase()
    {
        //clears old list of programs
        ProgramRemoveComboBox.Items.Clear();
        courseAddProgramNameComboBox.Items.Clear();
        programCourseRemoveComboBox.Items.Clear();
        try
        {
            var connectionString = "Server=localhost;Database=bospro;Uid=root;Pwd=;";
            var conn = new MySqlConnection(connectionString);

            var sqlQuery = "SELECT `program_name` FROM `programs`;";

            var query = new MySqlCommand(sqlQuery, conn);
            conn.Open();
            var reader = query.ExecuteReader();

            while (reader.Read())
            {
                ProgramRemoveComboBox.Items.Add(reader.GetString("program_name"));
                courseAddProgramNameComboBox.Items.Add(reader.GetString("program_name"));
                programCourseRemoveComboBox.Items.Add(reader.GetString("program_name"));
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
            //connection string assigned the database file address path
            var MyConnection2 = "Server=localhost;Database=bospro;Uid=root;Pwd=;";

            //Taking user-input from the text fields
            var pcode = programCode.Text;
            var pname = programName.Text;
            var pcollege = programCollege.Text;

            //This is the insert query in which we're taking input from the user 
            var Query = "INSERT INTO `programs`(`program_code`, `program_name`, `program_college`) VALUES('" + pcode + "', '" + pname + "', '" + pcollege + "');";

            //This is  MySqlConnection here we'll create the object and pass my connection string.
            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);

            //This is command class which will handle the query and connection object.
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.
            await databaseOkDialog.ShowAsync();

            //Emptying the text fields
            programCode.Text = String.Empty;
            programName.Text = String.Empty;
            programCollege.Text = String.Empty;

            //Refreshing the db
            getProgramNamesFromDatabase();

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

    private async void delPrograms()
    {
        try
        {
            //This is my connection string i have assigned the database file address path
            var connectionString = "Server=localhost;Database=bospro;Uid=root;Pwd=;";
            var conn = new MySqlConnection(connectionString);

            //Getting the text from the combobox
            var pname = ProgramRemoveComboBox.SelectedItem;

            //This is the insert query in which we're taking input from the user 
            var Query = "DELETE FROM `programs` WHERE program_name='" + pname + "';";

            //This is  MySqlConnection here we'll create the object and pass my connection string.
            var MyConn2 = new MySqlConnection(connectionString);

            //This is command class which will handle the query and connection object.
            var MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();
            // Here our query will be executed and data saved into the database.
            await programDeleteDialog.ShowAsync();
            ProgramRemoveComboBox.Items.Clear();
            getProgramNamesFromDatabase();
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

    private void programCourseRemoveComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        courseRemoveComboBox.Items.Clear();
        //Add course names to corse comboBox in remove course pivot
        getCoursesFromDatabase();
        courseRemoveComboBox.IsEnabled = true;
    }

    private void AccountEditButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        AdminName.IsEnabled = true;
        AdminEmail.IsEnabled = true;
        AdminPassword.IsEnabled = true;
        AdminPassword.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
        AccountChangeSave.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
    }

    private void LogoutButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Frame.Navigate(typeof(MainPage));
    }
}