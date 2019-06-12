// Filename: ChallengeSetupFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class ChallengeSetupFaker : CustomFaker<ChallengeSetup>
    {
        private static readonly IEnumerable<MoneyEntryFee> MoneyEntryFees = ValueObject.GetDeclaredOnlyFields<MoneyEntryFee>();

        private static readonly IEnumerable<TokenEntryFee> TokenEntryFees = ValueObject.GetDeclaredOnlyFields<TokenEntryFee>();

        private static readonly IEnumerable<EntryFee> EntryFees = MoneyEntryFees.Cast<EntryFee>().Union(TokenEntryFees);

        public ChallengeSetupFaker()
        {
            this.CustomInstantiator(
                faker =>
                {
                    var bestOf = faker.PickRandom(ValueObject.GetDeclaredOnlyFields<BestOf>());

                    var payoutEntries = faker.PickRandom(ValueObject.GetDeclaredOnlyFields<PayoutEntries>());

                    if (EntryFeeCurrency != null)
                    {
                        if (EntryFeeCurrency == CurrencyType.Money)
                        {
                            return new ChallengeSetup(bestOf, payoutEntries, faker.PickRandom(MoneyEntryFees));
                        }

                        if (EntryFeeCurrency == CurrencyType.Token)
                        {
                            return new ChallengeSetup(bestOf, payoutEntries, faker.PickRandom(TokenEntryFees));
                        }
                    }

                    return new ChallengeSetup(bestOf, payoutEntries, faker.PickRandom(EntryFees));
                }
            );
        }

        private CurrencyType EntryFeeCurrency { get; set; }

        public ChallengeSetup FakeSetup(CurrencyType entryFeeCurrency = null)
        {
            EntryFeeCurrency = entryFeeCurrency;

            return this.Generate();
        }
    }
}
