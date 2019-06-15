// Filename: TransactionFailure.cs
// Date Created: 2019-06-01
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
    public sealed class TransactionFailure : ValueObject
    {
        public TransactionFailure(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }

        public override string ToString()
        {
            return Message;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Message;
        }
    }
}
