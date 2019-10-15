// Filename: ChallengeDataSet.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using Bogus;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers.DataSets
{
    public class ChallengeDataSet
    {
        public ChallengeDataSet(Faker faker)
        {
            Faker = faker;
        }

        public Faker Faker { get; }

        public ChallengeId Id()
        {
            return ChallengeId.FromGuid(Faker.Random.Guid());
        }

        public PayoutEntries PayoutEntries()
        {
            return Faker.PickRandom(ValueObject.GetValues<PayoutEntries>());
        }

        public EntryFee EntryFee(Currency? entryFeeCurrency = null)
        {
            var moneyEntryFees = ValueObject.GetValues<MoneyEntryFee>().ToList();

            var tokenEntryFees = ValueObject.GetValues<TokenEntryFee>().ToList();

            if (entryFeeCurrency != null)
            {
                if (entryFeeCurrency == Currency.Money)
                {
                    Faker.PickRandom(moneyEntryFees);
                }

                if (entryFeeCurrency == Currency.Token)
                {
                    Faker.PickRandom(tokenEntryFees);
                }
            }

            return Faker.PickRandom(moneyEntryFees.Cast<EntryFee>().Union(tokenEntryFees));
        }
    }
}
