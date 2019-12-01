// Filename: Country.cs
// Date Created: 2019-10-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

namespace eDoxa.Seedwork.Domain.Misc
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class Country : Enumeration<Country>
    {
        public static readonly Country Canada = new Country(
            1,
            "CA",
            "CAN",
            "1");
        public static readonly Country UnitedStates = new Country(
            1 << 1,
            "US",
            "USA",
            "1");

        public Country()
        {
            ThreeDigitIso = string.Empty;
            CallingCode = string.Empty;
        }

        private Country(
            int value,
            string name,
            string threeDigitIso,
            string callingCode
        ) : base(value, name)
        {
            ThreeDigitIso = threeDigitIso;
            CallingCode = callingCode;
        }

        public string TwoDigitIso => Name;

        public string ThreeDigitIso { get; }

        public string CallingCode { get; }
    }
}
