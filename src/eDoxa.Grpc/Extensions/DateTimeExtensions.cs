// Filename: DateTimeExtensions.cs
// Date Created: 2019-12-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Google.Protobuf.WellKnownTypes;

namespace eDoxa.Grpc.Extensions
{
    public static class DateTimeExtensions
    {
        public static Timestamp? ToTimestampUtc(this DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ToTimestamp();
        }

        public static Timestamp? ToTimestampUtcOrNull(this DateTime? dateTime)
        {
            return dateTime.HasValue ? DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc).ToTimestamp() : null;
        }
    }
}
