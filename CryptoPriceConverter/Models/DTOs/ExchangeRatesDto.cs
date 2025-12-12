namespace CryptoPriceConverter.Models.DTOs
{
    // This class represents currency exchange rates.
    // It will only contain the specific currency rates needed for conversion.
    public class ExchangeRatesDto
    {
        public decimal USD { get; set; }
        public decimal EUR { get; set; }
        public decimal BRL { get; set; }
        public decimal GBP { get; set; }
        public decimal AUD { get; set; }
    }
}
