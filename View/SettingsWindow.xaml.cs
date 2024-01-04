using System.Windows;
using WPF_App_For_Checking_Prices.ViewModel;

namespace WPF_App_For_Checking_Prices.View;

public partial class SettingsWindow : Window
{
    private SettingsViewModel viewModel;

    public SettingsWindow(SettingsViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        DataContext = viewModel;
    }

    private void Button_Save_Click(object sender, RoutedEventArgs e)
    {
        viewModel.ApplyChanges();
        this.Close();
    }
}