using CryptoPriceConverter.Models;
using CryptoPriceConverter.Models.DTOs;
using System.Text.Json;

namespace CryptoPriceConverter.Services
{
    // Service class for fetching cryptocurrency prices.
    public class CryptoService : ICryptoService
    {
        // HttpClient instance for making HTTP requests.
        private readonly HttpClient _httpClient;

        // API key for authenticating with the crypto price API provider.
        private readonly string _apiKey = "b70def3721ec49b1a7a0c539252cf7a6";


        // ===========================================================


        // Constructor to initialize HttpClient via Dependency Injection.
        public CryptoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        // ===========================================================


        // Method to get cryptocurrency price asynchronously.
        public async Task<CryptoPriceDto> GetCryptoPriceAsync(string cryptoCode, string currencyCode)
        {
            // API endpoint URL for fetching latest cryptocurrency listings.
            var url = $"https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest ";


            // API key must be in the header as opposed to query string for this provider.
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _apiKey);

            
            // Make the HTTP GET request to the API.
            var apiResponse = await _httpClient.GetAsync(url);


            // Ensure the response indicates success.
            apiResponse.EnsureSuccessStatusCode();


            // Convert the response content to a JSON string.
            var responseJsonString = await apiResponse.Content.ReadAsStringAsync();


            // Deserialize the JSON string into the model object.
            var responseObject = JsonSerializer.Deserialize<CryptoPricesModel>(responseJsonString, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true  // Ignore case when matching JSON properties to C# properties
            });


            // Find the crypto by symbol. For instance: "symbol": "BTC"
            var cryptoSymbol = responseObject!.Data
                .FirstOrDefault(c => c.GetProperty("symbol").GetString()!
                .Equals(cryptoCode, StringComparison.OrdinalIgnoreCase));


            // If the crypto symbol is not found, throw an exception.
            if (cryptoSymbol.ValueKind == JsonValueKind.Undefined)
                throw new Exception($"Crypto {cryptoCode} not found.");


            // Extract the crypto price in the requested currency
            var cryptoPrice = cryptoSymbol
                .GetProperty("quote")
                .GetProperty(currencyCode)
                .GetProperty("price")
                .GetDecimal();


            // Create and return the DTO with the crypto symbol and price.
            // This DTO contains only the relevant information for the client.
            CryptoPriceDto cryptoPriceDto = new()
            {
                Symbol = cryptoSymbol.GetProperty("symbol").ToString()!,
                Price = cryptoPrice
            };


            return cryptoPriceDto;
        }
    }
}
