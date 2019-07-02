// Filename: TransactionDescription.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
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
