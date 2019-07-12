// Filename: ChallengeFaker.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers
{
    public sealed class ChallengeFaker : Faker<IChallenge>
    {
        public ChallengeFaker()
        {
            this.CustomInstantiator(
                faker =>
                {
                    var entryFee = MoneyEntryFee.Five;

                    var payout = new PayoutFactory().CreateInstance().GetPayout(PayoutEntries.Ten, entryFee);

                    var challenge = new Challenge(entryFee, payout);

                    challenge.SetEntityId(faker.Challenge().Id());

                    return challenge;
                }
            );
        }
    }
}
