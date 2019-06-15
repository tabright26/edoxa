// Filename: ParticipantFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers.Extensions;
using eDoxa.Seedwork.Common.Extensions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    internal sealed class ParticipantFaker : Faker<Participant>
    {
        private MatchFaker _matchFaker;

        public ParticipantFaker(Challenge challenge)
        {
            this.CustomInstantiator(faker => new Participant(challenge, faker.UserId(), faker.UserGameReference(challenge.Game)));

            this.RuleFor(participant => participant.Id, faker => faker.ParticipantId());

            this.RuleFor(
                participant => participant.Matches,
                (faker, participant) =>
                {
                    _matchFaker = _matchFaker ?? new MatchFaker(participant);

                    _matchFaker.UseSeed(faker.Random.Int());

                    return participant.Challenge.State != ChallengeState.Inscription
                        ? new HashSet<Match>(_matchFaker.Generate(faker.Random.Int(1, participant.Challenge.Setup.BestOf + 3)))
                        : new HashSet<Match>();
                }
            );

            this.RuleFor(participant => participant.Timestamp, (faker, participant) => faker.ParticipantTimestamp(participant.Challenge));

            this.RuleFor(participant => participant.LastSync, (faker, participant) => participant.Matches.Max(match => match.Timestamp as DateTime?));
        }

        [NotNull]
        public override Participant Generate(string ruleSets = null)
        {
            _matchFaker = null;

            return base.Generate(ruleSets);
        }
    }
}
