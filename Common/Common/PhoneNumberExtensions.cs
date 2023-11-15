using System;
using System.Collections.Generic;
using System.Linq;
using PhoneNumbers;

namespace Common.Helpers
{
    /// <summary>
    /// country code extensions
    /// </summary>
    public static class PhoneNumberExtensions
    {
        /// <summary>
        /// get all countries with id , country code and international prefix
        /// </summary>
        /// <returns></returns>
        public static List<PhoneNumber> GetCountryCodes()
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();

            var phoneNumberUtilRegions = PhoneNumbers.PhoneNumberUtil.GetInstance().GetSupportedRegions();
            return phoneNumberUtilRegions.Select(c => new PhoneNumber()
            {
                CountryCode = phoneNumberUtil.GetMetadataForRegion(c).CountryCode,
                Id = phoneNumberUtil.GetMetadataForRegion(c).Id,
                InternationalPrefix = phoneNumberUtil.GetMetadataForRegion(c).InternationalPrefix,
                HasPreferredInternationalPrefix = phoneNumberUtil.GetMetadataForRegion(c).HasPreferredInternationalPrefix,
                PreferredInternationalPrefix = phoneNumberUtil.GetMetadataForRegion(c).PreferredInternationalPrefix,

            }).ToList();
        }


        /// <summary>
        /// get the especial country by id , country code and international prefix
        /// </summary>
        /// <returns></returns>
        public static PhoneNumber GetCountryInformationById(string id)
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            var metaData = phoneNumberUtil.GetMetadataForRegion(id);
            return new PhoneNumber()
            {
                CountryCode = metaData.CountryCode,
                Id = metaData.Id,
                InternationalPrefix = metaData.InternationalPrefix,
                HasPreferredInternationalPrefix = metaData.HasPreferredInternationalPrefix,
                PreferredInternationalPrefix = metaData.PreferredInternationalPrefix,

            };
        }


        /// <summary>
        /// get the especial country by code , country code and international prefix
        /// </summary>
        /// <returns></returns>
        public static PhoneNumber GetCountryInformationByCode(int code)
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            var metaData = phoneNumberUtil.GetMetadataForNonGeographicalRegion(code);

            return new PhoneNumber()
            {
                CountryCode = metaData.CountryCode,
                Id = metaData.Id,
                InternationalPrefix = metaData.InternationalPrefix,
                HasPreferredInternationalPrefix = metaData.HasPreferredInternationalPrefix,
                PreferredInternationalPrefix = metaData.PreferredInternationalPrefix,

            };
        }


        /// <summary>
        /// get all countries with id , country code and international prefix
        /// </summary>
        /// <returns></returns>
        public static List<PhoneNumber> GetCountryCodesLimited(List<string> countryIds, int count = 50, int offset = 0)
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();

            var phoneNumberUtilRegions = PhoneNumbers.PhoneNumberUtil.GetInstance().GetSupportedRegions().ToList();
            if (countryIds != null && countryIds.Count > 0)
                phoneNumberUtilRegions = phoneNumberUtilRegions.Where(countryIds.Contains).ToList();
            var metaDta = phoneNumberUtilRegions.Select(c => phoneNumberUtil.GetMetadataForRegion(c)).Skip(offset)
                .Take(count).ToList();

            return metaDta.Select(c => new PhoneNumber()
            {
                CountryCode = c.CountryCode,
                Id = c.Id,
                InternationalPrefix = c.InternationalPrefix,
                HasPreferredInternationalPrefix = c.HasPreferredInternationalPrefix,
                PreferredInternationalPrefix = c.PreferredInternationalPrefix,

            }).ToList();
        }
        /// <summary>
        /// get the list of country code helper by country codes
        /// </summary>
        /// <param name="countryCodes"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static IEnumerable<PhoneNumber> GetCountryCodesLimited(List<int> countryCodes, int count = 50, int offset = 0)
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();

            var phoneNumberUtilRegions = PhoneNumbers.PhoneNumberUtil.GetInstance().GetSupportedGlobalNetworkCallingCodes().ToList();

            if (countryCodes != null && countryCodes.Count > 0)
                phoneNumberUtilRegions = phoneNumberUtilRegions.Where(countryCodes.Contains).ToList();

            var metaDta = phoneNumberUtilRegions
                .Select(c => phoneNumberUtil.GetMetadataForNonGeographicalRegion(c))
                .Skip(offset)
                .Take(count).ToList();
            return metaDta.Select(c => new PhoneNumber()
            {
                CountryCode = c.CountryCode,
                Id = c.Id,
                InternationalPrefix = c.InternationalPrefix,
                HasPreferredInternationalPrefix = c.HasPreferredInternationalPrefix,
                PreferredInternationalPrefix = c.PreferredInternationalPrefix,

            }).ToList();
        }

        /// <summary>
        /// get the all information on phone number 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static PhoneNumber GetInformationOfPhoneNumberNormalized(string phoneNumber)
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();

            var phoneNumberFinder = phoneNumberUtil.FindNumbers(phoneNumber, null);
            var phoneNumberMatches = phoneNumberFinder as PhoneNumberMatch[] ?? phoneNumberFinder.ToArray();
            if (phoneNumberMatches?.FirstOrDefault() == null)
                throw new ArgumentOutOfRangeException(nameof(phoneNumber),
                    "invalid phone number to fine country code and international prefix");

            var metaData = phoneNumberMatches?.FirstOrDefault();
            return new PhoneNumber()
            {
                Id = phoneNumberUtil
                    .GetMetadataForNonGeographicalRegion(metaData.Number.CountryCode)
                    .Id,
                CountryCode = metaData.Number.CountryCode,
                HasPreferredInternationalPrefix = phoneNumberUtil
                    .GetMetadataForNonGeographicalRegion(metaData.Number.CountryCode)
                    .HasPreferredInternationalPrefix,

                InternationalPrefix = phoneNumberUtil
                    .GetMetadataForNonGeographicalRegion(metaData.Number.CountryCode)
                    .InternationalPrefix,
                PreferredInternationalPrefix = phoneNumberUtil
                    .GetMetadataForNonGeographicalRegion(metaData.Number.CountryCode)
                    .PreferredInternationalPrefix,
            };
        }
    }
}