// Filename: UtcNowDateTimeProvider.cs
// Date Created: 2019-07-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Domain
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
