// Filename: ICurrency.cs
// Date Created: 2019-05-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Seedwork.Domain.Common.Abstactions
{
    public interface ICurrency
    {
        CurrencyType Type { get; }

        decimal Amount { get; }
    }
}
