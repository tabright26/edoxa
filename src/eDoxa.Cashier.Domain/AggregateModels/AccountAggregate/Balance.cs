// Filename: Balance.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
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

        public override string ToString()
        {
            return CurrencyType.ToString();
        }
    }
}
