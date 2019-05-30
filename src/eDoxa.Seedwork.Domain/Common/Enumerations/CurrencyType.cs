// Filename: Currency.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Seedwork.Domain.Common.Enumerations
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class CurrencyType : Enumeration<CurrencyType>
    {
        public static readonly CurrencyType Money = new CurrencyType(1, nameof(Money));
        public static readonly CurrencyType Token = new CurrencyType(2, nameof(Token));

        public CurrencyType()
        {
        }

        private CurrencyType(int value, string name) : base(value, name)
        {
        }
    }
}
