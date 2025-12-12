using CryptoPriceConverter.Models.DTOs;

namespace CryptoPriceConverter.Services
{
    // Service for converting a cryptocurrency price to various fiat currencies.
    public class CryptoToCurrencyConversionService : ICryptoToCurrencyConversionService
    {
        public ConvertedCryptoPricesDto ConvertCryptoToCurrencies(ExchangeRatesDto exchangeRatesDto, CryptoPriceDto cryptoPricesDto)
        {
            // Perform currency conversion using the provided exchange rates.
            ConvertedCryptoPricesDto convertedCryptoPricesDto = new()
            {
                CryptoPriceInUSD = cryptoPricesDto.Price, // The crypto price is received in US dollar by default.
                CryptoPriceInEUR = cryptoPricesDto.Price * (exchangeRatesDto.EUR / exchangeRatesDto.USD),
                CryptoPriceInBRL = cryptoPricesDto.Price * (exchangeRatesDto.BRL / exchangeRatesDto.USD),
                CryptoPriceInGBP = cryptoPricesDto.Price * (exchangeRatesDto.GBP / exchangeRatesDto.USD),
                CryptoPriceInAUD = cryptoPricesDto.Price * (exchangeRatesDto.AUD / exchangeRatesDto.USD)
            };


            return convertedCryptoPricesDto;
        }
    }
}
