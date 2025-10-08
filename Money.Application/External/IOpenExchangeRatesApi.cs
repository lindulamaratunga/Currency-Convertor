using Refit;

namespace Money.Application.External
{
    public interface IOpenExchangeRatesApi
    {
        [Get("/latest.json")]
        Task<OpenExchangeRatesResponse> GetLatestRateAsync(
            [AliasAs("app_id")] string appId,
            [AliasAs("base")] string baseCurrency,
            [AliasAs("symbols")] string symbols);
    }
}
