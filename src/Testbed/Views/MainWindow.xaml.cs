using ControlzEx.Theming;
using Testbed.ViewModels;

namespace Testbed.Views;
public partial class MainWindow
{
    public MainWindow()
    {
        DataContext = new MainViewModel();
        InitializeComponent();

        ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
        ThemeManager.Current.SyncTheme();
    }
}
