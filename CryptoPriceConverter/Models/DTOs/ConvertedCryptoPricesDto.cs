namespace CryptoPriceConverter.Models.DTOs
{
    // This class represents converted cryptocurrency prices in various fiat currencies.
    public class ConvertedCryptoPricesDto
    {
        public decimal CryptoPriceInUSD { get; set; }
        public decimal CryptoPriceInEUR { get; set; }
        public decimal CryptoPriceInBRL { get; set; }
        public decimal CryptoPriceInGBP { get; set; }
        public decimal CryptoPriceInAUD { get; set; }
    }
}
