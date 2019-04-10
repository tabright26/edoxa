// Filename: BirthDate.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Seedwork.Domain.Common.ValueObjects
{
    public sealed class BirthDate : ValueObject
    {
        private const int MajorAge = 18;
        public static readonly BirthDate Default = new BirthDate();

        private DateTime? _date;

        public BirthDate()
        {
            _date = null;
        }

        public BirthDate(int year, int month, int day) : this()
        {
            this.Change(year, month, day);
        }

        /// <summary>
        ///     The <see cref="BirthDate" /> year.
        /// </summary>
        public int Year
        {
            get
            {
                return _date?.Year ?? 1;
            }
        }

        /// <summary>
        ///     The <see cref="BirthDate" /> month.
        /// </summary>
        public int Month
        {
            get
            {
                return _date?.Month ?? 1;
            }
        }

        /// <summary>
        ///     The <see cref="BirthDate" /> day.
        /// </summary>
        public int Day
        {
            get
            {
                return _date?.Day ?? 1;
            }
        }

        /// <summary>
        ///     Initialize a new <see cref="BirthDate" /> based on a <see cref="DateTime" /> object.
        /// </summary>
        /// <param name="date">The <see cref="DateTime" />.</param>
        /// <returns>The new <see cref="BirthDate" /> intance.</returns>
        public static BirthDate FromDate(DateTime date)
        {
            return new BirthDate(date.Year, date.Month, date.Day);
        }

        public override string ToString()
        {
            return this.ToDate().ToString("yyyy-MM-dd");
        }

        /// <summary>
        ///     Reinitialize an instance of the <see cref="BirthDate" /> class.
        /// </summary>
        /// <param name="year">The <see cref="BirthDate" /> year.</param>
        /// <param name="month">The <see cref="BirthDate" /> month.</param>
        /// <param name="day">The <see cref="BirthDate" /> day.</param>
        /// <remarks>
        ///     This method is required by EF Core context class to be able to update OwnedEntityType.
        /// </remarks>
        public void Change(int year, int month, int day)
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

        /// <summary>
        ///     Evaluate if the <see cref="BirthDate" /> represent a major person.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="BirthDate" /> represent a major person, if not false.
        /// </returns>
        public bool IsMajor()
        {
            return DateTime.UtcNow.Year - Year >= MajorAge;
        }

        /// <summary>
        ///     Initialize a new <see cref="DateTime" /> based on a <see cref="BirthDate" /> object.
        /// </summary>
        /// <returns>The new <see cref="DateTime" /> intance.</returns>
        public DateTime ToDate()
        {
            return new DateTime(Year, Month, Day);
        }
    }
}