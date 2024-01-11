using System.ComponentModel;
using WPF_App_For_Checking_Prices.Model;
using System.Windows;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WPF_App_For_Checking_Prices.ViewModel;
class SerialPortViewModel : INotifyPropertyChanged
{
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
        serialPortModel = new SerialPortModel();
        serialPortModel.DataReceived += SerialPortModel_DataReceived;
    }                      

    private void SerialPortModel_DataReceived(string data)
    {
        Application.Current.Dispatcher.Invoke(async () =>
        {
            ScannedData = data;
            await SendDataToApiAsync(data);
        });
    }

    private async Task SendDataToApiAsync(string data)
    {
        using (var client = new HttpClient())
        {
            string apiUrl = $"https://localhost:7204/Price?EAN={data}";
            try
            {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ProductInfo = JsonConvert.DeserializeObject<ProductInformation>(content);
                }
                else
                {
                    MessageBox.Show("Error fetching data from API");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception occurred: {ex.Message}");
            }
        }
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
