// Filename: StringExtensions.cs
// Date Created: 2019-05-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

namespace eDoxa.Seedwork.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string ToPascalcase(this string str)
        {
            return str.Length > 1 ? str.First().ToString().ToUpper() + str.Substring(1) : string.Empty;
        }

        public static string ToCamelcase(this string str)
        {
            return str.Length > 1 ? str.First().ToString().ToLower() + str.Substring(1) : string.Empty;
        }
    }
}
