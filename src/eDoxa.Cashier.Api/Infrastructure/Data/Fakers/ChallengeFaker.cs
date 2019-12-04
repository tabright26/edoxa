// Filename: ChallengeFaker.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using Bogus;

using eDoxa.Cashier.Api.Factories;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers.Abstractions;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers
{
    public sealed partial class ChallengeFaker : IChallengeFaker
    {
        public IReadOnlyCollection<IChallenge> FakeChallenges(int count)
        {
            return this.Generate(count);
        }

        public IChallenge FakeChallenge()
        {
            return this.Generate();
        }
    }

    public sealed partial class ChallengeFaker : Faker<IChallenge>
    {
        public ChallengeFaker()
        {
            this.CustomInstantiator(
                faker =>
                {
                    var entryFee = MoneyEntryFee.Five;

                    var payout = new ChallengePayoutFactory().CreateInstance().GetPayout(PayoutEntries.Ten, entryFee);

                    return new Challenge(faker.Challenge().Id(), entryFee, payout);
                });
        }
    }
}
