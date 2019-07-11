// Filename: ICurrency.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public interface ICurrency
    {
        Currency Type { get; }

        decimal Amount { get; }
    }
}
