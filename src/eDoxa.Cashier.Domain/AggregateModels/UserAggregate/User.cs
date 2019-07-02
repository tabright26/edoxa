// Filename: User.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.Validators;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class User : Entity<UserId>, IAggregateRoot
    {
        public User(UserId userId, string connectAccountId, string customerId)
        {
            ConnectAccountId = connectAccountId;
            CustomerId = customerId;
            BankAccountId = null;
            this.SetEntityId(userId);
        }

        public string ConnectAccountId { get; }

        public string CustomerId { get; }

        [CanBeNull]
        public string BankAccountId { get; private set; }

        public bool HasBankAccount()
        {
            return BankAccountId != null;
        }

        public void AddBankAccount(string bankAccountId)
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
