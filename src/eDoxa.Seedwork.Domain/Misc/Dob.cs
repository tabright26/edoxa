// Filename: Dob.cs
// Date Created: 2019-10-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Domain.Misc
{
    [JsonObject]
    public sealed class Dob : ValueObject
    {
        public Dob(DateTime date) : this(date.Year, date.Month, date.Day)
        {
        }

        [JsonConstructor]
        public Dob(int year, int month, int day) : this()
        {
            Year = year;
            Month = month;
            Day = day;
        }

        private Dob()
        {
            // Required by EF core.
        }

        [JsonProperty]
        public int Year { get; private set; } // Required by EF core.

        [JsonProperty]
        public int Month { get; private set; } // Required by EF core.

        [JsonProperty]
        public int Day { get; private set; } // Required by EF core.

        public override string ToString()
        {
            return new DateTime(Year, Month, Day).ToString("yy-MM-dddd");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Year;
            yield return Month;
            yield return Day;
        }
    }
}
