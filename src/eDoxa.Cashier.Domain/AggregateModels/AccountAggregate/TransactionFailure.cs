// Filename: TransactionFailure.cs
// Date Created: 2019-05-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class TransactionFailure
    {
        private string _value;

        public TransactionFailure(string message)
        {
            _value = message;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
