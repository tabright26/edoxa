// Filename: TransactionFaker.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Common.Abstactions;

namespace eDoxa.Cashier.Api.Application.Data.Fakers
{
    public sealed class TransactionFaker : CustomFaker<Transaction>
    {
        private const string PositiveTransaction = nameof(PositiveTransaction);
        private const string NegativeTransaction = nameof(NegativeTransaction);

        private readonly Currency[] _currencies =
        {
            Money.Five,
            Money.Ten,
            Money.Twenty,
            Money.TwentyFive,
            Money.Fifty,
            Money.OneHundred,
            Money.TwoHundred,
            Money.FiveHundred,
            Token.FiftyThousand,
            Token.OneHundredThousand,
            Token.TwoHundredFiftyThousand,
            Token.FiveHundredThousand,
            Token.OneMillion,
            Token.FiveMillions
        };

        public TransactionFaker()
        {
            this.StrictMode(true);

            this.RuleSet(
                PositiveTransaction,
                ruleSet =>
                {
                    ruleSet.RuleFor(transaction => transaction.Id, faker => TransactionId.FromGuid(faker.Random.Guid()));

                    ruleSet.RuleFor(transaction => transaction.Timestamp, faker => faker.Date.Recent());

                    ruleSet.RuleFor(transaction => transaction.Currency, faker => faker.PickRandom(_currencies));

                    ruleSet.RuleFor(
                        transaction => transaction.Type,
                        (faker, transaction) =>
                        {
                            var types = TransactionType.GetAll().ToList();

                            if (transaction.Currency is Money)
                            {
                                types.Remove(TransactionType.Reward);
                            }

                            types.Remove(TransactionType.Charge);

                            types.Remove(TransactionType.Withdraw);

                            return faker.PickRandom(types);
                        }
                    );

                    ruleSet.RuleFor(
                        transaction => transaction.Status,
                        (faker, transaction) =>
                        {
                            var status = TransactionStatus.GetAll().ToList();

                            status.Remove(TransactionStatus.Pending);

                            return faker.PickRandom(status);
                        }
                    );

                    ruleSet.RuleFor(transaction => transaction.Description, faker => new TransactionDescription(faker.Lorem.Sentence()));

                    ruleSet.RuleFor(
                        transaction => transaction.Failure,
                        (faker, transaction) => transaction.Status == TransactionStatus.Failed ? new TransactionFailure(faker.Lorem.Sentence()) : null
                    );
                }
            );

            this.RuleSet(
                NegativeTransaction,
                ruleSet =>
                {
                    ruleSet.RuleFor(transaction => transaction.Id, faker => TransactionId.FromGuid(faker.Random.Guid()));

                    ruleSet.RuleFor(transaction => transaction.Timestamp, faker => faker.Date.Recent());

                    ruleSet.RuleFor(transaction => transaction.Currency, faker => -faker.PickRandom(_currencies));

                    ruleSet.RuleFor(
                        transaction => transaction.Type,
                        (faker, transaction) =>
                        {
                            var types = TransactionType.GetAll().ToList();

                            if (transaction.Currency is Token)
                            {
                                types.Remove(TransactionType.Withdraw);
                            }

                            types.Remove(TransactionType.Deposit);

                            types.Remove(TransactionType.Reward);

                            types.Remove(TransactionType.Payout);

                            return faker.PickRandom(types);
                        }
                    );

                    ruleSet.RuleFor(
                        transaction => transaction.Status,
                        (faker, transaction) =>
                        {
                            var status = TransactionStatus.GetAll().ToList();

                            status.Remove(TransactionStatus.Pending);

                            return faker.PickRandom(status);
                        }
                    );

                    ruleSet.RuleFor(transaction => transaction.Description, faker => new TransactionDescription(faker.Lorem.Sentence()));

                    ruleSet.RuleFor(
                        transaction => transaction.Failure,
                        (faker, transaction) => transaction.Status == TransactionStatus.Failed ? new TransactionFailure(faker.Lorem.Sentence()) : null
                    );
                }
            );
        }

        public List<Transaction> FakePositiveTransactions(int count)
        {
            return this.Generate(count, PositiveTransaction);
        }

        public Transaction FakePositiveTransaction()
        {
            return this.Generate(PositiveTransaction);
        }

        public List<Transaction> FakeNegativeTransactions(int count)
        {
            return this.Generate(count, NegativeTransaction);
        }

        public Transaction FakeNegativeTransaction()
        {
            return this.Generate(NegativeTransaction);
        }
    }
}
