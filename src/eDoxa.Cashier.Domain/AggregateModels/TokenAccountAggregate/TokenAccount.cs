// Filename: TokenAccount.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Functional.Maybe;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

using Serilog;

namespace eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate
{
    public class TokenAccount : Entity<AccountId>, ITokenAccount, IAggregateRoot
    {
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

        public IReadOnlyCollection<TokenTransaction> Transactions => _transactions;

        public Token Balance => new Token(Transactions.Sum(transaction => transaction.Amount));

        public Token Pending => new Token(Transactions.Where(transaction => transaction.Pending).Sum(transaction => transaction.Amount));

        public ITokenTransaction Deposit(Token amount)
        {
            var transaction = new TokenTransaction(amount);

            _transactions.Add(transaction);

            return transaction;
        }

        public Option<ITokenTransaction> TryRegister(Token amount, ServiceId serviceId)
        {
            if (Balance < amount)
            {
                return new Option<ITokenTransaction>();
            }

            var transaction = new TokenPendingTransaction(-amount, serviceId);

            if (!_transactions.Add(transaction))
            {
                return new Option<ITokenTransaction>();
            }

            Log.Information($"{Id} register to {serviceId} amount {amount} - balance {Balance}");

            return new Option<ITokenTransaction>(transaction);
        }

        public Option<ITokenTransaction> TryPayoff(Token amount, ServiceId serviceId)
        {
            return Transactions.Where(transaction => transaction.Pending && transaction.ServiceId == serviceId.ToString())
                .Select(transaction => this.TryPayoff(amount, transaction))
                .DefaultIfEmpty(new Option<ITokenTransaction>())
                .Single();
        }

        private Option<ITokenTransaction> TryPayoff(Token amount, ITokenTransaction transaction)
        {
            return transaction.TryPayoff(amount).Select(payoff =>
            {
                if (!_transactions.Add(payoff))
                {
                    return new Option<ITokenTransaction>();
                }

                Log.Information($"{Id} deposit amount {amount} - balance {Balance}");

                return new Option<ITokenTransaction>(payoff);
            }).Single();
        }
    }
}