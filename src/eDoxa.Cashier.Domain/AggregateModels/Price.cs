// Filename: Price.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public sealed class Price : Money
    {
        public Price(Currency currency) : base(currency.Type == CurrencyType.Token ? new Token(currency.Amount).ToMoney() : currency)
        {
        }
    }
}
