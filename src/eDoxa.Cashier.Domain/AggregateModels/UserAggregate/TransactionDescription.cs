// Filename: TransactionDescription.cs
// Date Created: 2019-04-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class TransactionDescription
    {
        public static readonly TransactionDescription FundsAdded = new TransactionDescription("eDoxa funds");
        public static readonly TransactionDescription FundsWithdrawal = new TransactionDescription("eDoxa withdrawal");
        public static readonly TransactionDescription TokensBought = new TransactionDescription("eDoxa tokens");

        private readonly string _description;

        public TransactionDescription(string description)
        {
            _description = description;
        }

        public override string ToString()
        {
            return _description;
        }
    }
}