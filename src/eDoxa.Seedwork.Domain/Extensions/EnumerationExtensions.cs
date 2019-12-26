// Filename: EnumerationExtensions.cs
// Date Created: 2019-12-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Seedwork.Domain.Extensions
{
    public static class EnumerationExtensions
    {
        public static TEnum ToEnum<TEnum>(this IEnumeration enumeration)
        {
            return (TEnum) Enum.ToObject(typeof(TEnum), enumeration.Value);
        }

        public static TEnum ToEnumOrDefault<TEnum>(this IEnumeration? enumeration)
        {
            return (TEnum) Enum.ToObject(typeof(TEnum), enumeration?.Value ?? 0);
        }
    }
}
