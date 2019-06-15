// Filename: CurrencyType.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Attributes;

namespace eDoxa.Seedwork.Common.Enumerations
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class CurrencyType : Enumeration<CurrencyType>
    {
        [AllowValue(true)] public static readonly CurrencyType Money = new CurrencyType(1, nameof(Money));
        [AllowValue(true)] public static readonly CurrencyType Token = new CurrencyType(2, nameof(Token));

        public CurrencyType()
        {
        }

        private CurrencyType(int value, string name) : base(value, name)
        {
        }
    }
}
