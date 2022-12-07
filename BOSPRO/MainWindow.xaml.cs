using BOSPRO.Helpers;

namespace BOSPRO;

public sealed partial class MainWindow : WindowEx
{
    public MainWindow()
    {
        InitializeComponent();

        //AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/WindowIcon.ico"));
        Content = null;
        //Title = "AppDisplayName".GetLocalized();
        ExtendsContentIntoTitleBar = true;
    }
}
