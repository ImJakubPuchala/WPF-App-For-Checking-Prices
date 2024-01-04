using System.Configuration;
using System.Data;
using System.Windows;
using WPF_App_For_Checking_Prices.Model;

namespace WPF_App_For_Checking_Prices
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Initialization code here (if any)
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Place your cleanup code here
            // Example: Log that the application is closing or release resources
            //new SerialPortModel().Close();

            base.OnExit(e);
        }
    }
}
