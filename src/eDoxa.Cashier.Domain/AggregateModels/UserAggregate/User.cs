// Filename: User.cs
// Date Created: 2019-05-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate.Specifications;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Entities;
using eDoxa.Seedwork.Domain.Validations;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class User : Entity<UserId>, IAggregateRoot
    {
        private StripeAccountId _accountId;
        [CanBeNull] private StripeBankAccountId _bankAccountId;
        private StripeCustomerId _customerId;
        private MoneyAccount _moneyAccount;
        private TokenAccount _tokenAccount;

        public User(UserId userId, StripeAccountId accountId, StripeCustomerId customerId) : this()
        {
            Id = userId;
            _accountId = accountId;
            _customerId = customerId;
            _moneyAccount = new MoneyAccount(this);
            _tokenAccount = new TokenAccount(this);
        }

        private User()
        {
            _bankAccountId = null;
        }

        public StripeAccountId AccountId => _accountId;

        public StripeCustomerId CustomerId => _customerId;

        [CanBeNull] public StripeBankAccountId BankAccountId => _bankAccountId;

        public MoneyAccount MoneyAccount => _moneyAccount;

        public TokenAccount TokenAccount => _tokenAccount;

        public void AddBankAccount(StripeBankAccountId bankAccountId)
        {
            if (!this.CanAddBankAccount())
            {
                throw new InvalidOperationException();
            }

            _bankAccountId = bankAccountId;
        }

        public void RemoveBankAccount()
        {
            if (!this.CanRemoveBankAccount())
            {
                throw new InvalidOperationException();
            }

            _bankAccountId = null;
        }

        public ValidationResult CanAddBankAccount()
        {
            var result = new ValidationResult();

            if (new HasBankAccountSpecification().IsSatisfiedBy(this))
            {
                result.AddError("A bank account is already associated with this account.");
            }

            return result;
        }

        public ValidationResult CanRemoveBankAccount()
        {
            var result = new ValidationResult();

            if (new HasBankAccountSpecification().Not().IsSatisfiedBy(this))
            {
                result.AddError("No bank account is associated with this account.");
            }

            return result;
        }
    }
}
