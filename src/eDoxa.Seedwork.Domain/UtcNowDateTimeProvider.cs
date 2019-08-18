// Filename: UtcNowDateTimeProvider.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Seedwork.Domain
{
    public sealed class UtcNowDateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime _dateTime;

        public UtcNowDateTimeProvider()
        {
            _dateTime = DateTime.UtcNow;
        }

        public DateTime DateTime => _dateTime;
    }
}
