// Filename: BirthDate.cs
// Date Created: 2019-04-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class BirthDate : ValueObject
    {
        private const int MajorAge = 18;

        private DateTime _date;

        public BirthDate(int year, int month, int day) : this()
        {
            try
            {
                _date = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                throw new ValidationException("BirthDate is not valid.", exception);
            }
        }

        private BirthDate()
        {
            // Required by EF Core.
        }

        public int Year => _date.Year;

        public int Month => _date.Month;

        public int Day => _date.Day;

        public static BirthDate FromDate(DateTime date)
        {
            return new BirthDate(date.Year, date.Month, date.Day);
        }

        public override string ToString()
        {
            return this.ToDate().ToString("yyyy-MM-dd");
        }

        public bool IsMajor()
        {
            return DateTime.UtcNow.Year - Year >= MajorAge;
        }

        public DateTime ToDate()
        {
            return new DateTime(Year, Month, Day);
        }
    }
}