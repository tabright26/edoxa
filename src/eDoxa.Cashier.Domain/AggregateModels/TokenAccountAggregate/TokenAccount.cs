// Filename: TokenAccount.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate.Specifications;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Validations;

namespace eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate
{
    public class TokenAccount : Entity<AccountId>, ITokenAccount, IAggregateRoot
    {
        private DateTime? _lastDeposit;
        private HashSet<TokenTransaction> _transactions;
        private UserId _userId;

        public TokenAccount(UserId userId) : this()
        {
            _userId = userId;
        }

        private TokenAccount()
        {
            _transactions = new HashSet<TokenTransaction>();
        }

        public UserId UserId => _userId;

        public Token Balance =>
            new Token(Transactions
                .Where(transaction => transaction.Status.Equals(TransactionStatus.Completed))
                .Sum(transaction => transaction.Amount));

        public Token Pending =>
            new Token(Transactions
                .Where(transaction => transaction.Status.Equals(TransactionStatus.Pending))
                .Sum(transaction => transaction.Amount));

        public DateTime? LastDeposit => _lastDeposit;

        public IReadOnlyCollection<TokenTransaction> Transactions => _transactions;

        public ITokenTransaction Deposit(Token amount)
        {
            if (!this.CanDeposit())
            {
                throw new InvalidOperationException();
            }
            
            var transaction = new TokenDepositTransaction(amount);

            _transactions.Add(transaction);

            return transaction;
        }

        public ValidationResult CanDeposit()
        {
            var result = new ValidationResult();

            if (new DailyTokenDepositUnavailableSpecification().IsSatisfiedBy(this))
            {
                result.AddError($"Deposit unavailable until {LastDeposit?.AddDays(1)}");
            }

            return result;
        }

        public ITokenTransaction Reward(Token amount)
        {
            var transaction = new TokenRewardTransaction(amount);

            _transactions.Add(transaction);

            return transaction;
        }

        public ITokenTransaction Charge(Token amount)
        {
            if (!this.CanCharge(amount))
            {
                throw new InvalidOperationException();
            }

            var transaction = new TokenChargeTransaction(amount);

            _transactions.Add(transaction);

            return transaction;
        }

        public ValidationResult CanCharge(Token token)
        {
            var result = new ValidationResult();

            if (new InsufficientTokenSpecification(token).IsSatisfiedBy(this))
            {
                result.AddError("Insufficient tokens.");
            }

            return result;
        }

        public ITokenTransaction Payout(Token amount)
        {
            var transaction = new TokenPayoutTransaction(amount);

            _transactions.Add(transaction);

            return transaction;
        }

        public ITokenTransaction CompleteTransaction(ITokenTransaction transaction)
        {
            transaction.Complete();

            if (transaction.Type.Equals(TransactionType.Deposit))
            {
                _lastDeposit = DateTime.UtcNow;
            }

            return transaction;
        }

        public ITokenTransaction FailureTransaction(ITokenTransaction transaction, string message)
        {
            transaction.Fail(message);

            return transaction;
        }
    }
}