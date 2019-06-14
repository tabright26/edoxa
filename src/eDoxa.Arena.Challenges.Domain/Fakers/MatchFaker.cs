// Filename: MatchFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers.Extensions;
using eDoxa.Seedwork.Common.Abstactions;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class MatchFaker : CustomFaker<Match>
    {
        public MatchFaker(Participant participant)
        {
            this.CustomInstantiator(
                faker => new Match(participant, faker.MatchReference(participant.Challenge.Game), faker.MatchStats(participant.Challenge.Game))
            );

            this.RuleFor(match => match.Id, faker => faker.MatchId());

            this.RuleFor(match => match.Timestamp, faker => faker.MatchTimestamp());
        }
    }
}
