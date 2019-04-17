// Filename: Transaction.cs
// Date Created: 2019-04-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public class Transaction : Entity<TransactionId>
    {
        public const decimal TaxPercent = 15M;

        private TransactionDescription _description;
        private Money _price;
        private TransactionType _type;
        private User _user;

        protected Transaction(User user, Money price, TransactionDescription description, TransactionType type) : this()
        {
            _user = user;
            _description = description;
            _price = price;
            _type = type;
        }

        private Transaction()
        {
            // Required by EF Core.
        }

        public User User => _user;

        public Money Price => _price;

        public TransactionDescription Description => _description;

        public TransactionType Type => _type;
    }
}