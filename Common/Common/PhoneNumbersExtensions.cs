using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    /// <summary>
    /// Some of the Extensions and helper to validate and manipulate phoneNumber
    /// </summary>
    public static class PhoneNumbersExtensions
    {
        /// <summary>
        /// Try to get phoneNumber and normalize phoneNumber format
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static string TryToGetFromPhoneNumber(string phoneNumber)
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            var phoneNumberFinder = phoneNumberUtil.FindNumbers(phoneNumber, null);
            if (phoneNumberFinder?.FirstOrDefault() != null)
            {
                phoneNumber =
                    $"{phoneNumberFinder.FirstOrDefault().Number.CountryCode}{phoneNumberFinder.FirstOrDefault().Number.NationalNumber}";

                return phoneNumber;
            }

            throw new ArgumentException("invalid phone number");
        }

        /// <summary>
        /// get the international prefix by given the country code 
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public static string GetInternationalPrefix(int countryCode)
        {
            var countryInformation = PhoneNumberExtensions.GetCountryInformationByCode(countryCode);
            return countryInformation.InternationalPrefix;
        }
    }
}