// Filename: UtcNowDateTimeProvider.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Seedwork.Domain.Providers
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
