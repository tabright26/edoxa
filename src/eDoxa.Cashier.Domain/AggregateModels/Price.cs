// Filename: Price.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public sealed class Price : Money
    {
        private const decimal TokenToMoneyFactor = 1000M; // FRANCIS: To refactor.

        public Price(Currency currency) : base(currency.Type == CurrencyType.Token ? Math.Abs(currency.Amount) / TokenToMoneyFactor : Math.Abs(currency.Amount))
        {
        }
    }
}
