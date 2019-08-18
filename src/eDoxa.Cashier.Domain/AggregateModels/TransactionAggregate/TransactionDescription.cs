// Filename: TransactionDescription.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    public sealed class TransactionDescription : ValueObject
    {
        public TransactionDescription(string text)
        {
            Text = text;
        }

        public string Text { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
