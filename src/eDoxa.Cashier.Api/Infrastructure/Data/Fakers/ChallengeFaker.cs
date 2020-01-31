// Filename: ChallengeFaker.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;

using Bogus;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers.Abstractions;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
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
                faker => new Challenge(
                    faker.Challenge().Id(),
                    new ChallengePayoutFactory().CreateInstance().GetChallengePayout(ChallengePayoutEntries.Ten, MoneyEntryFee.Five)));
        }
    }
}
