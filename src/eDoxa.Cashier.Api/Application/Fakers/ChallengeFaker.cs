// Filename: ChallengeFaker.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Api.Application.Fakers.Extensions;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Api.Application.Fakers
{
    public sealed class ChallengeFaker : Faker<IChallenge>
    {
        public ChallengeFaker()
        {
            this.CustomInstantiator(
                faker =>
                {
                    var payout = new PayoutFactory().CreateInstance().GetPayout(PayoutEntries.Ten, MoneyEntryFee.Five);

                    var challenge = new Challenge(payout);

                    challenge.SetEntityId(faker.Challenge().Id());
                    
                    return challenge;
                }
            );
        }
    }
}
