// Filename: User.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Validators;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class User : Entity<UserId>, IAggregateRoot
    {
        public User(UserId userId, StripeAccountId accountId, StripeCustomerId customerId) : this()
        {
            Id = userId;
            AccountId = accountId;
            CustomerId = customerId;
            Account = new Account(this);
        }

        private User()
        {
            BankAccountId = null;
        }

        public StripeAccountId AccountId { get; private set; }

        public StripeCustomerId CustomerId { get; private set; }

        [CanBeNull] public StripeBankAccountId BankAccountId { get; private set; }

        public Account Account { get; private set; }

        public void AddBankAccount(StripeBankAccountId bankAccountId)
        {
            if (!this.CanAddBankAccount())
            {
                throw new InvalidOperationException();
            }

            BankAccountId = bankAccountId;
        }

        public void RemoveBankAccount()
        {
            if (!this.CanRemoveBankAccount())
            {
                throw new InvalidOperationException();
            }

            BankAccountId = null;
        }

        public bool CanAddBankAccount()
        {
            return new AddBankAccountValidator().Validate(this).IsValid;
        }

        public bool CanRemoveBankAccount()
        {
            return new RemoveBankAccountValidator().Validate(this).IsValid;
        }
    }
}
