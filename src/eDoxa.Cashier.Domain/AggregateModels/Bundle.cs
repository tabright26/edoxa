using System.Collections.Generic;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public sealed class Bundle : ValueObject
    {
        public Bundle(ICurrency currency, Price price)
        {
            Currency = currency;
            Price = price;
        }

        public ICurrency Currency { get; }

        public Price Price { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Currency;
            yield return Price;
        }

        public override string ToString()
        {
            return Currency.ToString()!;
        }
    }
}
