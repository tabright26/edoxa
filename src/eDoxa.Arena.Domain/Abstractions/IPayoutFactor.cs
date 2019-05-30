// Filename: IPayoutTemplate.cs
// Date Created: 2019-05-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Domain.Abstractions
{
    public interface IPayoutFactor
    {
        IPayout CreatePayout(EntryFee entryFee, CurrencyType currencyType);
    }
}
