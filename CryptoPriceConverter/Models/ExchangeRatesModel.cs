namespace CryptoPriceConverter.Models
{
    // This class represents currency exchange rates model.
    // It will contain all data returned from the exchange rates API.
    // The properties correspond to the JSON structure of the API response.
    // The property names match the JSON keys for automatic deserialization.
    public class ExchangeRatesModel
    {
        public string Base { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; } = new();
    }
}
