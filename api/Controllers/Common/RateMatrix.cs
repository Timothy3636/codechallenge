public static class RateMatrix
{
    public static Dictionary<string, Dictionary<string, decimal>> Matrix { get; } = new Dictionary<string, Dictionary<string, decimal>>()
    {
        { "AUD", new Dictionary<string, decimal>()
            {
                { "JPY", 80.5m },
                { "USD", 0.74m },
                { "HKD", 5.77m },
                { "EUR", 0.62m },
                { "GBP", 0.53m }
            }
        },
        { "JPY", new Dictionary<string, decimal>()
            {
                { "AUD", 0.012m },
                { "USD", 0.0092m },
                { "HKD", 0.071m },
                { "EUR", 0.0077m },
                { "GBP", 0.0066m }
            }
        },
        // Add other currencies and rates here...
    };
}
