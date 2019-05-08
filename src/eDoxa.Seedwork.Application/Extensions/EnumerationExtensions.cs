// Filename: EnumerationExtensions.cs
// Date Created: 2019-05-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using static eDoxa.Seedwork.Domain.Aggregate.Enumeration;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class Enumeration
    {
        public static TEnumeration FromAnyDisplayName<TEnumeration>(string displayName)
        where TEnumeration : Domain.Aggregate.Enumeration, new()
        {
            if (displayName == None<TEnumeration>().DisplayName)
            {
                return None<TEnumeration>();
            }

            if (displayName == All<TEnumeration>().DisplayName)
            {
                return All<TEnumeration>();
            }

            return FromDisplayName<TEnumeration>(displayName);
        }
    }
}