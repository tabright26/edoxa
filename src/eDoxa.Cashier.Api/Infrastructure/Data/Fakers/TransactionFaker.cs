// Filename: TransactionFaker.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers
{
    public sealed partial class TransactionFaker : ITransactionFaker
    {
        public IReadOnlyCollection<ITransaction> FakeTransactions(int count, string? ruleSets = null)
        {
            return this.Generate(count, ruleSets);
        }

        public ITransaction FakeTransaction(string? ruleSets = null)
        {
            return this.Generate(ruleSets);
        }
    }

    public sealed partial class TransactionFaker : Faker<ITransaction>
    {
        public const string PositiveTransaction = nameof(PositiveTransaction);
        public const string NegativeTransaction = nameof(NegativeTransaction);

        private readonly ICurrency[] _currencies =
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
            this.RuleSet(
                PositiveTransaction,
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(
                        faker =>
                        {
                            var currency = faker.PickRandom(_currencies);

                            var types = TransactionType.GetEnumerations().ToList();

                            if (currency is Money)
                            {
                                types.Remove(TransactionType.Reward);
                            }

                            types.Remove(TransactionType.Charge);

                            types.Remove(TransactionType.Withdrawal);

                            var transaction = new Transaction(
                                currency,
                                new TransactionDescription(faker.Lorem.Sentence()),
                                faker.PickRandom(types),
                                new DateTimeProvider(faker.Date.Recent()));

                            transaction.SetEntityId(TransactionId.FromGuid(faker.Random.Guid()));

                            var statuses = TransactionStatus.GetEnumerations().ToList();

                            statuses.Remove(TransactionStatus.Pending);

                            var status = faker.PickRandom(statuses);

                            if (status == TransactionStatus.Succeded)
                            {
                                transaction.MarkAsSucceded();
                            }

                            if (status == TransactionStatus.Failed)
                            {
                                transaction.MarkAsFailed();
                            }

                            return transaction;
                        });
                });

            this.RuleSet(
                NegativeTransaction,
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(
                        faker =>
                        {
                            var currency = faker.PickRandom(_currencies);

                            var types = TransactionType.GetEnumerations().ToList();

                            if (currency is Token)
                            {
                                types.Remove(TransactionType.Withdrawal);
                            }

                            types.Remove(TransactionType.Deposit);

                            types.Remove(TransactionType.Reward);

                            types.Remove(TransactionType.Payout);

                            var transaction = new Transaction(
                                currency,
                                new TransactionDescription(faker.Lorem.Sentence()),
                                faker.PickRandom(types),
                                new DateTimeProvider(faker.Date.Recent()));

                            transaction.SetEntityId(TransactionId.FromGuid(faker.Random.Guid()));

                            var statuses = TransactionStatus.GetEnumerations().ToList();

                            statuses.Remove(TransactionStatus.Pending);

                            var status = faker.PickRandom(statuses);

                            if (status == TransactionStatus.Succeded)
                            {
                                transaction.MarkAsSucceded();
                            }

                            if (status == TransactionStatus.Failed)
                            {
                                transaction.MarkAsFailed();
                            }

                            return transaction;
                        });
                });
        }
    }
}
