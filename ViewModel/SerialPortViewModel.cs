using System.ComponentModel;
using WPF_App_For_Checking_Prices.Model;
using System.Windows;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using WPF_App_For_Checking_Prices.Service;

namespace WPF_App_For_Checking_Prices.ViewModel;
class SerialPortViewModel : INotifyPropertyChanged
{
    private ApiService _apiService;
    public event PropertyChangedEventHandler PropertyChanged;
    private SerialPortModel serialPortModel;
    private string scannedData;
    private ProductInformation productInfo;

    public string ScannedData
    {
        get => scannedData;
        set
        {
            scannedData = value;
            OnPropertyChanged(nameof(ScannedData));
        }
    }

    public ProductInformation ProductInfo
    {
        get => productInfo;
        set
        {
            productInfo = value;
            OnPropertyChanged(nameof(ProductInfo));
        }
    }

    public SerialPortViewModel()
    {
        _apiService = new ApiService();
        serialPortModel = new SerialPortModel();
        serialPortModel.DataReceived += SerialPortModel_DataReceived;
    }                      

    private void SerialPortModel_DataReceived(string data)
    {
        Application.Current.Dispatcher.Invoke(async () =>
        {
            ScannedData = data;
            try
            {
                ProductInfo = await _apiService.GetProductInformationAsync(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception occurred: {ex.Message}");
            }
        });
    }

    public void ChangeSerialPort(string portName)
    {
        serialPortModel.Close();
        serialPortModel = new SerialPortModel(portName);
        serialPortModel.DataReceived += SerialPortModel_DataReceived;
        serialPortModel.Open();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
