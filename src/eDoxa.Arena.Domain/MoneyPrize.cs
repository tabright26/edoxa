// Filename: MoneyPrize.cs
// Date Created: 2019-05-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Domain.Abstractions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Domain
{
    public sealed class MoneyPrize : Prize
    {
        public MoneyPrize(decimal amount) : base(amount, CurrencyType.Money)
        {
        }
    }
}
