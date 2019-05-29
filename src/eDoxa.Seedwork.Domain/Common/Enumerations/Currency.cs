﻿// Filename: Currency.cs
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
    public sealed class Currency : Enumeration<Currency>
    {
        public static readonly Currency Money = new Currency(1, nameof(Money));
        public static readonly Currency Token = new Currency(2, nameof(Token));

        public Currency()
        {
        }

        private Currency(int value, string name) : base(value, name)
        {
        }
    }
}
