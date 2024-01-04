using System.ComponentModel;
using System.Windows;
using System.IO;
using WPF_App_For_Checking_Prices.Model;

namespace WPF_App_For_Checking_Prices.ViewModel;

public class SettingsViewModel : INotifyPropertyChanged
{
    private const string ConfigFilePath = "config.txt";
    public event PropertyChangedEventHandler PropertyChanged;
    private string[] availablePorts;
    private string selectedPort;

    public string[] AvailablePorts
    {
        get
        {
            if (availablePorts == null)
            {
                availablePorts = new SerialPortModel().GetAllSerialPorts();
            }
            return availablePorts;
        }
    }

    public string SelectedPort
    {
        get => selectedPort;
        set
        {
            selectedPort = value;
            OnPropertyChanged(nameof(SelectedPort));
        }
    }

    public void ApplyChanges()
    {
        var serialPortViewModel = (SerialPortViewModel)Application.Current.MainWindow.DataContext;
        serialPortViewModel.ChangeSerialPort(SelectedPort);
        SaveSelectedPort();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private void SaveSelectedPort()
    {
        File.WriteAllText(ConfigFilePath, SelectedPort);
    }

    public void LoadSelectedPort()
    {
        if (File.Exists(ConfigFilePath))
        {
            SelectedPort = File.ReadAllText(ConfigFilePath);
        }
    }
}