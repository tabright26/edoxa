// Filename: Dob.cs
// Date Created: 2019-10-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class UserDob : ValueObject
    {
        public UserDob(DateTime date) : this(date.Year, date.Month, date.Day)
        {
        }

        public UserDob(int year, int month, int day) : this()
        {
            Year = year;
            Month = month;
            Day = day;
        }

        private UserDob()
        {
            // Required by EF core.
        }

        public int Year { get; private set; } // Required by EF core.

        public int Month { get; private set; } // Required by EF core.

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
