// Filename: ChallengeSetupDataSet.cs
// Date Created: 2019-06-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.Fakers.DataSets
{
    public class ChallengeSetupDataSet
    {
        public ChallengeSetupDataSet(Faker faker)
        {
            Faker = faker;
        }

        internal Faker Faker { get; }

        public BestOf BestOf()
        {
            return Faker.PickRandom(ValueObject.GetAllowValues<BestOf>());
        }

        public PayoutEntries PayoutEntries()
        {
            return Faker.PickRandom(ValueObject.GetAllowValues<PayoutEntries>());
        }

        public EntryFee EntryFee(CurrencyType entryFeeCurrency = null)
        {
            var moneyEntryFees = ValueObject.GetAllowValues<MoneyEntryFee>().ToList();

            var tokenEntryFees = ValueObject.GetAllowValues<TokenEntryFee>().ToList();

            if (entryFeeCurrency != null)
            {
                if (entryFeeCurrency == CurrencyType.Money)
                {
                    Faker.PickRandom(moneyEntryFees);
                }

                if (entryFeeCurrency == CurrencyType.Token)
                {
                    Faker.PickRandom(tokenEntryFees);
                }
            }

            return Faker.PickRandom(moneyEntryFees.Cast<EntryFee>().Union(tokenEntryFees));
        }
    }
}
