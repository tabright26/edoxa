// Filename: ChallengeFaker.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using Bogus;

using eDoxa.Cashier.Api.Areas.Challenges.Factories;
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

                    var payout = new PayoutFactory().CreateInstance().GetPayoutAsync(PayoutEntries.Ten, entryFee).Result;

                    var challenge = new Challenge(entryFee, payout);

                    challenge.SetEntityId(faker.Challenge().Id());

                    return challenge;
                });
        }
    }
}
