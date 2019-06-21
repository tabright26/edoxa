// Filename: UtcNowDateTimeProvider.cs
// Date Created: 2019-06-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.Common
{
    public sealed class UtcNowDateTimeProvider : IDateTimeProvider
    {
        public DateTime DateTime => DateTime.UtcNow;
    }
}
