using System.Windows;
using WPF_App_For_Checking_Prices.View;
using WPF_App_For_Checking_Prices.ViewModel;

namespace WPF_App_For_Checking_Prices;

public partial class MainWindow : Window
{
    private SerialPortViewModel serialPortViewModel;
    private SettingsWindow settingsWindow;

    public MainWindow()
    {
        InitializeComponent();
        serialPortViewModel = new SerialPortViewModel();
        DataContext = serialPortViewModel;

        SettingsViewModel settingsViewModel = new SettingsViewModel();
        settingsViewModel.LoadSelectedPort();
        if (!string.IsNullOrEmpty(settingsViewModel.SelectedPort))
        {
            serialPortViewModel.ChangeSerialPort(settingsViewModel.SelectedPort);
        }
    }

    private void OpenSettings_Click(object sender, RoutedEventArgs e)
    {
        if (settingsWindow == null || !settingsWindow.IsLoaded)
        {
            SettingsViewModel settingsViewModel = new SettingsViewModel();
            settingsWindow = new SettingsWindow(settingsViewModel);
            settingsWindow.Closed += (s, args) => settingsWindow = null;
            settingsWindow.Show();
        }
    }
}
