// Filename: EnumerationErrorDescribers.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;

namespace eDoxa.Seedwork.Application.FluentValidation.ErrorDescribers
{
    public static class EnumerationErrorDescribers
    {
        public static string NotNull<TEnumeration>()
        where TEnumeration : Enumeration<TEnumeration>, new()
        {
            return $"The enumeration {typeof(TEnumeration).Name} is required.";
        }

        public static string NotAll<TEnumeration>()
        where TEnumeration : Enumeration<TEnumeration>, new()
        {
            return
                $"The enumeration {typeof(TEnumeration).Name} cannot be All (-1). These are valid enumeration names: [{string.Join(", ", Enumeration<TEnumeration>.GetEnumerations())}].";
        }

        public static string IsInEnumeration<TEnumeration>()
        where TEnumeration : Enumeration<TEnumeration>, new()
        {
            return
                $"The enumeration {typeof(TEnumeration).Name} is invalid. These are valid enumeration names: [{string.Join(", ", Enumeration<TEnumeration>.GetEnumerations())}].";
        }
    }
}
