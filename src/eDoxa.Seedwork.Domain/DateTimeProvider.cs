// Filename: DateTimeProvider.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Seedwork.Domain
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime _dateTime;

        public DateTimeProvider(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public DateTime DateTime => _dateTime;
    }
}
