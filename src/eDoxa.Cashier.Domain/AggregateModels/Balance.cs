// Filename: Balance.cs
// Date Created: 2019-05-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public sealed class Balance : ValueObject
    {
        public Balance(IReadOnlyCollection<Transaction> transactions, CurrencyType currencyType)
        {
            Available = transactions.Where(transaction => transaction.Currency.Type == currencyType && transaction.Status == TransactionStatus.Completed)
                .Sum(transaction => transaction.Currency.Amount);

            Pending = transactions.Where(transaction => transaction.Currency.Type == currencyType && transaction.Status == TransactionStatus.Pending)
                .Sum(transaction => transaction.Currency.Amount);

            CurrencyType = currencyType;
        }

        public decimal Available { get; }

        public decimal Pending { get; }

        public CurrencyType CurrencyType { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Available;
            yield return Pending;
            yield return CurrencyType;
        }
    }
}
