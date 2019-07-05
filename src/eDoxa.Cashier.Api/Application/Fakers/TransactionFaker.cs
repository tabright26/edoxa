// Filename: TransactionFaker.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.Abstactions;

namespace eDoxa.Cashier.Api.Application.Fakers
{
    public sealed class TransactionFaker : CustomFaker<Transaction>
    {
        public const string PositiveTransaction = nameof(PositiveTransaction);
        public const string NegativeTransaction = nameof(NegativeTransaction);

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
                                new FakeDataTimeProvider(faker.Date.Recent())
                            );

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
                        }
                    );
                }
            );

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
                                new FakeDataTimeProvider(faker.Date.Recent())
                            );

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
                        }
                    );
                }
            );
        }

        private sealed class FakeDataTimeProvider : IDateTimeProvider
        {
            public FakeDataTimeProvider(DateTime dateTime)
            {
                DateTime = dateTime;
            }

            public DateTime DateTime { get; }
        }
    }
}
