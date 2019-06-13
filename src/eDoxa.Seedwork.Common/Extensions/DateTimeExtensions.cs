// Filename: DateTimeExtensions.cs
// Date Created: 2019-06-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime DateKeepHours(this DateTime dateTime)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                0,
                0
            );
        }
    }
}
