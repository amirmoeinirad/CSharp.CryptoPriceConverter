using CryptoPriceConverter.Models.DTOs;

namespace CryptoPriceConverter.Services
{
    // Interface for the Crypto to Currency Conversion Service.
    public interface ICryptoToCurrencyConversionService
    {
        // Converts cryptocurrency prices to various fiat currencies using exchange rates.
        public ConvertedCryptoPricesDto ConvertCryptoToCurrencies(ExchangeRatesDto exchangeRatesDto, CryptoPriceDto cryptoPricesDto);
    }
}
