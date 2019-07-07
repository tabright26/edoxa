// Filename: PersistentDateTimeProvider.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.Domain.Aggregate
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
