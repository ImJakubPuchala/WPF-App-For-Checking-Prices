using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WPF_App_For_Checking_Prices.Model;

namespace WPF_App_For_Checking_Prices.Service;
public class ApiService
{
    private HttpClient _client;

    public ApiService()
    {
        _client = new HttpClient();
    }

    public async Task<ProductInformation> GetProductInformationAsync(string eanCode)
    {
        string apiUrl = $"https://localhost:7204/Price?EAN={eanCode}";
        try
        {
            var response = await _client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductInformation>(content);
            }
            else
            { 
                return null;
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error fetching data from API: {ex.Message}", ex);
        }
    }
}
