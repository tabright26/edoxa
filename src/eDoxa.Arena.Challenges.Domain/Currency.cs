// Filename: PrizeType.cs
// Date Created: 2019-05-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    [TypeConverter(typeof(EnumerationConverter))]
    public sealed class Currency : Enumeration<Currency>
    {
        public static readonly Currency Money = new Currency(1, nameof(Money));
        public static readonly Currency Token = new Currency(2, nameof(Token));
        public static readonly Currency Undefined = new Currency(3, nameof(Undefined));

        private Currency(int value, string name) : base(value, name)
        {
        }
    }
}
