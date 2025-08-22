public static class UtilityHelper
{
    public static readonly HashSet<string> UKCountries = new HashSet<string> { "GB", "UK" };
    public static readonly HashSet<string> EUCountries = new HashSet<string>
    {
        "AT", "BE", "BG", "HR", "CY", "CZ", "DK", "EE", "FI", "FR", "DE", "EL",
        "HU", "IE", "IT", "LV", "LT", "LU", "MT", "NL", "PL", "PT", "RO", "SK", "SI", "ES", "SE"
    };

    private static readonly Dictionary<string, string> CountryCodeToNameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // United Kingdom
            { "GB", "United Kingdom" },
            { "UK", "United Kingdom" },
            
            // United States
            { "US", "United States" },
            
            // Canada
            { "CA", "Canada" },
            
            // EU Countries
            { "AT", "Austria" },
            { "BE", "Belgium" },
            { "BG", "Bulgaria" },
            { "HR", "Croatia" },
            { "CY", "Cyprus" },
            { "CZ", "Czech Republic" },
            { "DK", "Denmark" },
            { "EE", "Estonia" },
            { "FI", "Finland" },
            { "FR", "France" },
            { "DE", "Germany" },
            { "EL", "Greece" },
            { "GR", "Greece" }, // Alternative code for Greece
            { "HU", "Hungary" },
            { "IE", "Ireland" },
            { "IT", "Italy" },
            { "LV", "Latvia" },
            { "LT", "Lithuania" },
            { "LU", "Luxembourg" },
            { "MT", "Malta" },
            { "NL", "Netherlands" },
            { "PL", "Poland" },
            { "PT", "Portugal" },
            { "RO", "Romania" },
            { "SK", "Slovakia" },
            { "SI", "Slovenia" },
            { "ES", "Spain" },
            { "SE", "Sweden" }
        };

        /// <summary>
        /// Gets the country name for a given two-character country code.
        /// Supports countries in the UK, US, EU, and Canada.
        /// </summary>
        /// <param name="countryCode">Two-character country code (case-insensitive)</param>
        /// <returns>The country name if found, otherwise null</returns>
        public static string? GetCountryNameByCode(string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                return null;

            return CountryCodeToNameMap.TryGetValue(countryCode.Trim(), out var countryName) 
                ? countryName 
                : null;
        }
}