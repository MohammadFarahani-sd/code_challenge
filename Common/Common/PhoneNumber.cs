namespace Common.Helpers
{
    /// <summary>
    /// the country code from libphonenumber-csharp\8.12.21 nuget package 
    /// </summary>
    public class PhoneNumber
    {
        /// <summary>
        /// the id of country
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// country code for phone for example 98 for iran and 1 for US and 44 for UK
        /// </summary>
        public int CountryCode { get; set; }

        /// <summary>
        /// international prefix like 00 for IR and 011 for US and 00 for UK
        /// </summary>
        public string InternationalPrefix { get;  set; } 

        /// <summary>
        /// the preferred international prefix state 
        /// </summary>
        public bool HasPreferredInternationalPrefix { get;  set; }


        /// <summary>
        /// for when has preferred international prefix
        /// </summary>
        public string PreferredInternationalPrefix { get;  set; }

    }
}