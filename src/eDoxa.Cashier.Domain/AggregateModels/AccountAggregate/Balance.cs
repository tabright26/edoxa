// Filename: Balance.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class Balance : ValueObject
    {
        public Balance(IReadOnlyCollection<ITransaction> transactions, Currency currency)
        {
            Available = transactions.Where(transaction => transaction.Currency.Type == currency && transaction.Status == TransactionStatus.Succeded)
                .Sum(transaction => transaction.Currency.Amount);

            Pending = transactions.Where(transaction => transaction.Currency.Type == currency && transaction.Status == TransactionStatus.Pending)
                .Sum(transaction => transaction.Currency.Amount);

            Currency = currency;
        }

        public decimal Available { get; }

        public decimal Pending { get; }

        public Currency Currency { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Available;
            yield return Pending;
            yield return Currency;
        }

        public override string ToString()
        {
            return Currency.ToString();
        }
    }
}
