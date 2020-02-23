// Filename: Balance.cs
// Date Created: 2019-06-25
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class Balance : ValueObject
    {
        // Francis: Il y a un erreur ici, je crois que on devrait soustraire les withdrawal aulieu de sum all.
        public Balance(IReadOnlyCollection<ITransaction> transactions, CurrencyType currencyType)
        {
            Available = transactions.Where(transaction => transaction.Currency.Type == currencyType && transaction.Status == TransactionStatus.Succeeded)
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
