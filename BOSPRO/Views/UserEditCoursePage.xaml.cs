using BOSPRO.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage;

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
        var savePicker = new FileSavePicker
        {
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary
        };

        // Dropdown of file types the user can save the file as
        savePicker.FileTypeChoices.Add("Rich Text", new List<string>() { ".rtf" });

        // Default file name if the user does not type one in or select a file to replace
        savePicker.SuggestedFileName = "New Document";

        StorageFile file = await savePicker.PickSaveFileAsync();
        if (file != null)
        {
            // Prevent updates to the remote version of the file until we
            // finish making changes and call CompleteUpdatesAsync.
            CachedFileManager.DeferUpdates(file);
            // write to file
            using (var randAccStream =
                await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
            {
                editor.Document.SaveToStream(Microsoft.UI.Text.TextGetOptions.FormatRtf, randAccStream);
            }

            // Let Windows know that we're finished changing the file so the
            // other app can update the remote version of the file.
            FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
            if (status != FileUpdateStatus.Complete)
            {
                var errorBox =
                    new Windows.UI.Popups.MessageDialog("File " + file.Name + " couldn't be saved.");
                await errorBox.ShowAsync();
            }
        }
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
            editor.Document.LoadFromStream(Microsoft.UI.Text.TextSetOptions.FormatRtf, randAccStream);
        }
    }
}
