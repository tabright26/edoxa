// Filename: BirthDate.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class BirthDate : ValueObject
    {
        private readonly DateTime _birthDate;

        public BirthDate(int year, int month, int day)
        {
            _birthDate = new DateTime(year, month, day);
        }

        public BirthDate(DateTime birthDate)
        {
            _birthDate = birthDate;
        }

        public int Year => _birthDate.Year;

        public int Month => _birthDate.Month;

        public int Day => _birthDate.Day;

        public static implicit operator DateTime(BirthDate birthDate)
        {
            return birthDate._birthDate;
        }

        public static IEnumerable<int> Years()
        {
            for (var index = 1925; index <= DateTime.Now.Year - 13; index++)
            {
                yield return index;
            }
        }

        public static IEnumerable<int> Months()
        {
            for (var index = 1; index <= 12; index++)
            {
                yield return index;
            }
        }

        public static IEnumerable<int> Days()
        {
            for (var index = 1; index <= 31; index++)
            {
                yield return index;
            }
        }

        public override string ToString()
        {
            return _birthDate.ToString("yyyy-MM-dd");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Year;
            yield return Month;
            yield return Day;
        }
    }
}
