// Filename: TypeExtensions.cs
// Date Created: 2019-05-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.Domain.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsEnumeration(this Type enumerationType)
        {
            return typeof(IEnumeration).IsAssignableFrom(enumerationType);
        }
    }
}