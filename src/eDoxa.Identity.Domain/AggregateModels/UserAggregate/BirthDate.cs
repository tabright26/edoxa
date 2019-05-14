// Filename: BirthDate.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

using IdentityModel;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class BirthDate : ValueObject
    {
        private int _day;
        private int _month;
        private int _year;

        public BirthDate(int year, int month, int day)
        {
            _year = year;
            _month = month;
            _day = day;
        }

        public int Year => _year;

        public int Month => _month;

        public int Day => _day;

        public DateTime ToDate()
        {
            return new DateTime(_year, _month, _day);
        }

        public override string ToString()
        {
            return this.ToDate().ToString("yyyy-MM-dd");
        }

        public UserClaim ToClaim()
        {
            return new UserClaim
            {
                ClaimType = JwtClaimTypes.BirthDate,
                ClaimValue = this.ToString()
            };
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
    }
}