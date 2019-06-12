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

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public class ChallengeFaker : CustomFaker<Challenge>
    {
        private readonly ChallengeSetupFaker _challengSetupFaker = new ChallengeSetupFaker();
        private readonly ChallengeTimelineFaker _challengeTimelineFaker = new ChallengeTimelineFaker();

        private ParticipantFaker _participantFaker;

        public ChallengeFaker()
        {
            this.CustomInstantiator(
                faker => new Challenge(
                    Game ?? faker.PickRandom(Game.GetAll()),
                    new ChallengeName(faker.Commerce.ProductName()),
                    _challengSetupFaker.FakeSetup(EntryFeeCurrency),
                    faker.PickRandom(ValueObject.GetDeclaredOnlyFields<ChallengeDuration>())
                )
            );

            this.RuleFor(challenge => challenge.Id, faker => ChallengeId.FromGuid(faker.Random.Guid()));

            this.RuleFor(challenge => challenge.Timeline, _ => _challengeTimelineFaker.FakeTimeline(State));

            this.RuleFor(
                challenge => challenge.Participants,
                (faker, challenge) =>
                {
                    _participantFaker = _participantFaker ?? new ParticipantFaker();

                    return ParticipantFaker.FakeParticipants(challenge.Setup.Entries, challenge);
                }
            );

            this.RuleFor(challenge => challenge.LastSync, (_, challenge) => challenge.Participants.Max(participant => participant.Timestamp as DateTime?));
        }

        public ParticipantFaker ParticipantFaker
        {
            get => _participantFaker;
            set
            {
                var matchFaker = _participantFaker.MatchFaker;

                value.MatchFaker = matchFaker;

                _participantFaker = value;
            }
        }

        private Game Game { get; set; }

        private ChallengeState State { get; set; }

        private CurrencyType EntryFeeCurrency { get; set; }

        public IEnumerable<Challenge> FakeChallenges(
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
