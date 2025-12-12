using System.Text.Json;

namespace CryptoPriceConverter.Models
{
    // Model class representing cryptocurrency prices response.
    // This class contains all data returned from the cryptocurrency API.
    public class CryptoPricesModel
    {
        // Why use JsonElement?
        // JsonElement is used here to allow for flexible handling of JSON data.
        // It enables working with dynamic or unknown JSON structures without defining
        // specific C# classes for every possible structure.
        public List<JsonElement> Data { get; set; } = new();
        public JsonElement Status { get; set; }
    }
}
