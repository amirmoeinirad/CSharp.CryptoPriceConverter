using CryptoPriceConverter.Models;
using CryptoPriceConverter.Models.DTOs;
using System.Text.Json;

namespace CryptoPriceConverter.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        // HttpClient instance for making API requests and receiving responses.
        private readonly HttpClient _httpClient;
        
        // API key for authenticating with the exchange rate service provider.
        private const string _apiKey = "acf564029149c4be5676cc17660abf5c";


        // ===========================================================


        // Constructor to initialize HttpClient via Dependency Injection.
        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;           
        }


        // ===========================================================


        // Method to get exchange rates asynchronously.
        public async Task<ExchangeRatesDto> GetExchangeRatesAsync()
        {
            // Construct the API URL with the access key.
            // API Key is included as a query parameter for authentication.
            var url = $"http://api.exchangeratesapi.io/v1/latest?access_key={_apiKey}";


            // Send HTTP GET request to the API.
            var apiResponse = await _httpClient.GetAsync(url);
            

            // Ensure the response indicates success.
            apiResponse.EnsureSuccessStatusCode();


            // Read the response content as a JSON string.
            // This operation must be awaited to avoid blocking.
            var responseJsonString = await apiResponse.Content.ReadAsStringAsync();


            // Deserialize JSON string into the model class
            var responseObject = JsonSerializer.Deserialize<ExchangeRatesModel>(responseJsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
            });

            var rates = responseObject!.Rates;


            // Map the relevant exchange rates data object to a DTO
            // This DTO contains only the required currency rates,
            // compared to the full API response model (ExchangeRatesModel).
            ExchangeRatesDto exchangeRatesDto = new()
            {
                USD = rates["USD"],
                EUR = rates["EUR"],
                BRL = rates["BRL"],
                GBP = rates["GBP"],
                AUD = rates["AUD"]
            };


            return exchangeRatesDto;
        }
    }
}
