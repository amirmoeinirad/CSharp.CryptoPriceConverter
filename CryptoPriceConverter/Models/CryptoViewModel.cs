using CryptoPriceConverter.Models.DTOs;

namespace CryptoPriceConverter.Models
{
    // This ViewModel encapsulates data for displaying cryptocurrency prices, exchange rates
    // and crypto code in index.cshtml.
    public class CryptoViewModel
    {
        public ExchangeRatesDto ExchangeRates { get; set; } = new ExchangeRatesDto();
        public ConvertedCryptoPricesDto ConvertedPrices { get; set; } = new ConvertedCryptoPricesDto();        
        public string CryptoCode { get; set; } = string.Empty;
    }
}
