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
using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers.Extensions;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public class ChallengeFaker : CustomFaker<Challenge>
    {
        private readonly ParticipantFaker _participantFaker = new ParticipantFaker();

        public ChallengeFaker()
        {
            this.CustomInstantiator(
                faker => new Challenge(faker.ChallengeGame(Game), faker.ChallengeName(), faker.ChallengeSetup(EntryFeeCurrency), faker.ChallengeDuration())
            );

            this.RuleFor(challenge => challenge.Id, faker => faker.ChallengeId());

            this.RuleFor(challenge => challenge.Timeline, (faker, challenge) => faker.ChallengeTimeline(challenge.Timeline.Duration, State));

            this.RuleFor(challenge => challenge.CreatedAt, (faker, challenge) => faker.ChallengeCreatedAt(challenge.Timeline));

            this.RuleFor(
                challenge => challenge.Participants,
                (faker, challenge) => _participantFaker.FakeParticipants(faker.ChallegeSetupEntries(challenge), challenge)
            );

            this.RuleFor(challenge => challenge.LastSync, (faker, challenge) => challenge.Participants.Max(participant => participant.Timestamp as DateTime?));
        }

        private Game Game { get; set; }

        private ChallengeState State { get; set; }

        private CurrencyType EntryFeeCurrency { get; set; }

        [NotNull]
        public override Faker<Challenge> UseSeed(int seed)
        {
            _participantFaker.UseSeed(seed);

            return base.UseSeed(seed);
        }

        public List<Challenge> FakeChallenges(
            int count,
            Game game = null,
            ChallengeState state = null,
            CurrencyType entryFeeCurrency = null
        )
        {
            Game = game;

            State = state;

            EntryFeeCurrency = entryFeeCurrency;

            return this.Generate(count);
        }

        public Challenge FakeChallenge(Game game = null, ChallengeState state = null, CurrencyType entryFeeCurrency = null)
        {
            Game = game;

            State = state;

            EntryFeeCurrency = entryFeeCurrency;

            return this.Generate();
        }
    }
}
