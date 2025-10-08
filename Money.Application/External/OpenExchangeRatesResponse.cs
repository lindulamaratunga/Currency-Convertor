using System.Text.Json.Serialization;

namespace Money.Application.External
{
    public class OpenExchangeRatesResponse
    {
        [JsonPropertyName("disclaimer")]
        public string Disclaimer { get; set; } = string.Empty;

        [JsonPropertyName("license")]
        public string License { get; set; } = string.Empty;

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; } = string.Empty;

        [JsonPropertyName("rates")]

        public required Dictionary<string, decimal> Rates { get; set; }
    }
}
