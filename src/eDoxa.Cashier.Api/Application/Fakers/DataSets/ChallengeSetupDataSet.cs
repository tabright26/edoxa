// Filename: ChallengeSetupDataSet.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using Bogus;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Api.Application.Fakers.DataSets
{
    public class ChallengeSetupDataSet
    {
        public ChallengeSetupDataSet(Faker faker)
        {
            Faker = faker;
        }

        internal Faker Faker { get; }

        public PayoutEntries PayoutEntries()
        {
            return Faker.PickRandom(ValueObject.GetValues<PayoutEntries>());
        }

        public EntryFee EntryFee(Currency entryFeeCurrency = null)
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
