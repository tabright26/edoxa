// Filename: ChallengeFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers.Extensions;
using eDoxa.Seedwork.Common.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public class ChallengeFaker : Faker<Challenge>
    {
        private ParticipantFaker _participantFaker;

        public ChallengeFaker(Game game = null, ChallengeState state = null, CurrencyType entryFeeCurrency = null)
        {
            this.UseSeed(8675309);

            this.CustomInstantiator(
                faker => new Challenge(
                    faker.ChallengeGame(game),
                    faker.ChallengeName(),
                    faker.ChallengeSetup(entryFeeCurrency),
                    faker.ChallengeDuration(),
                    localSeed
                )
            );

            this.RuleFor(challenge => challenge.Id, faker => faker.ChallengeId());

            this.RuleFor(challenge => challenge.Timeline, (faker, challenge) => faker.ChallengeTimeline(challenge.Timeline.Duration, state));

            this.RuleFor(
                challenge => challenge.Participants,
                (faker, challenge) =>
                {
                    _participantFaker = _participantFaker ?? new ParticipantFaker(challenge);

                    _participantFaker.UseSeed(faker.Random.Int());

                    return _participantFaker.Generate(faker.ChallegeSetupEntries(challenge));
                }
            );

            this.RuleFor(challenge => challenge.CreatedAt, (faker, challenge) => faker.ChallengeCreatedAt(challenge));

            this.RuleFor(
                challenge => challenge.LastSync,
                (faker, challenge) =>
                    challenge.Participants.SelectMany(participant => participant.Matches).Max(participant => participant.Timestamp as DateTime?)
            );
        }

        [NotNull]
        public override Challenge Generate(string ruleSets = null)
        {
            _participantFaker = null;

            return base.Generate(ruleSets);
        }
    }
}
