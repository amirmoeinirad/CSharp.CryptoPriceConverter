namespace CryptoPriceConverter.Models.DTOs
{
    // This class represents cryptocurrency prices.
    // It only contains the symbol and price of the cryptocurrency,
    // comared to the full model which contains all data from the API.
    public class CryptoPriceDto
    {
        public string Symbol { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
