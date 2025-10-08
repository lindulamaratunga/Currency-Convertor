namespace Money.Application.Services
{
    public interface IExchangeRateService
    {
        Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency);
    }
}
