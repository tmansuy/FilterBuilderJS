using System;

namespace SampleFunctions
{
    /// <summary>
    /// This class provides some functions used by the sample project.
    /// </summary>
    /// <remarks>It provides a simple example of how to write functions called from
    /// the Expression property of fields or other places such as field captions.</remarks>
    public static class SampleFunctions
    {
        /// <summary>
        /// Get the current tax rate.
        /// </summary>
        /// <returns>The current tax rate (hard-coded as 5%).</returns>
        public static decimal GetTaxRate()
        {
            return 0.05M;
        }

        /// <summary>
        /// Gets the tax on the specified amount.
        /// </summary>
        /// <param name="amount">The amount to calculate the tax for.</param>
        /// <returns>The tax (based on a hard-coded rate of 5%).</returns>
        public static decimal GetTax(decimal amount)
        {
            return GetTaxRate() * amount;
        }

        /// <summary>
        /// Combines the city, region, and postal code into a country-sensitive formatted address.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <param name="region">The region (state, province, etc.)</param>
        /// <param name="postalCode">The postal or zip code.</param>
        /// <param name="country">The country.</param>
        /// <returns>The country-sensitive formatted address.</returns>
        /// <remarks>For Germany, the format is PostalCode City Region. For all other
        /// countries, it's City, Region PostalCode.</remarks>
        public static string GetCSZ(string city, string region, string postalCode, string country)
        {
            string useRegion = region ?? "";
            string result;
            if (country == "Germany")
            {
                result = postalCode + " " + city + " " + useRegion;
            }
            else
            {
                result = city + ", " + useRegion + " " + postalCode;
            }
            return result;
        }
    }
}
