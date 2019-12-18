// Filename: EnumExtensions.cs
// Date Created: 2019-12-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Seedwork.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static TEnumeration ToEnumeration<TEnumeration>(this Enum @enum)
        where TEnumeration : Enumeration<TEnumeration>, new()
        {
            var enumeration = @enum.ToEnumerationOrDefault<TEnumeration>();

            return enumeration ?? throw new InvalidCastException();
        }

        public static TEnumeration? ToEnumerationOrDefault<TEnumeration>(this Enum @enum)
        where TEnumeration : Enumeration<TEnumeration>, new()
        {
            var value = (int) Enum.ToObject(@enum.GetType(), @enum);

            return value == 0 ? null : Enumeration<TEnumeration>.FromValue(value);
        }
    }
}
