// Filename: SetupFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class SetupFaker : CustomFaker<ChallengeSetup>
    {
        public SetupFaker()
        {
            this.RuleSet(
                CurrencyType.Money.ToString(),
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(
                        faker =>
                        {
                            var bestOf = faker.PickRandom(ValueObject.GetDeclaredOnlyFields<BestOf>());

                            var payoutEntries = faker.PickRandom(ValueObject.GetDeclaredOnlyFields<PayoutEntries>());

                            var entryFee = faker.PickRandom(ValueObject.GetDeclaredOnlyFields<MoneyEntryFee>());

                            return new ChallengeSetup(bestOf, payoutEntries, entryFee, new Entries(payoutEntries * 2));
                        }
                    );
                }
            );

            this.RuleSet(
                CurrencyType.Token.ToString(),
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(
                        faker =>
                        {
                            var bestOf = faker.PickRandom(ValueObject.GetDeclaredOnlyFields<BestOf>());

                            var payoutEntries = faker.PickRandom(ValueObject.GetDeclaredOnlyFields<PayoutEntries>());

                            var entryFee = faker.PickRandom(ValueObject.GetDeclaredOnlyFields<TokenEntryFee>());

                            return new ChallengeSetup(bestOf, payoutEntries, entryFee, new Entries(payoutEntries * 2));
                        }
                    );
                }
            );
        }

        public ChallengeSetup FakeSetup(CurrencyType currency)
        {
            var setup = this.Generate(currency.ToString());

            Console.WriteLine(setup.DumbAsJson());

            return setup;
        }
    }
}
