// Filename: FakeChallengeFactory.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Arena.Challenges.UnitTests.Utilities.Fakes
{
    public sealed partial class FakeChallengeFactory
    {
        private static readonly Lazy<FakeChallengeFactory> Lazy = new Lazy<FakeChallengeFactory>(() => new FakeChallengeFactory());

        public static FakeChallengeFactory Instance => Lazy.Value;

        public Challenge CreateChallenge(ChallengeSetup setup = null)
        {
            var setupFaker = new ChallengeSetupFaker();

            setup = setup ?? setupFaker.FakeSetup(CurrencyType.Money);

            var challenge = new Challenge(Game.LeagueOfLegends, new ChallengeName(nameof(Challenge)), setup, ChallengeDuration.OneDay);

            return challenge;
        }

        public Challenge CreateChallengeWithParticipant(UserId userId)
        {
            var challenge = this.CreateChallenge();

            challenge.RegisterParticipant(userId, new ExternalAccount(Guid.NewGuid()));

            return challenge;
        }

        public Challenge CreateChallengeWithParticipants(int? participantCount = null)
        {
            var challenge = this.CreateChallenge();

            participantCount = participantCount ?? challenge.Setup.Entries;

            for (var index = 0; index < participantCount; index++)
            {
                challenge.RegisterParticipant(new UserId(), new ExternalAccount(Guid.NewGuid()));
            }

            return challenge;
        }
    }

    public sealed partial class FakeChallengeFactory
    {
        public ChallengeSetup CreateChallengeSetup(BestOf bestOf)
        {
            return new ChallengeSetup(bestOf, PayoutEntries.TwentyFive, MoneyEntryFee.Five, new Entries(50));
        }
    }

    public sealed partial class FakeChallengeFactory
    {
        public Participant CreateParticipant()
        {
            return new Participant(new UserId(), new ExternalAccount(Guid.NewGuid()), BestOf.Five);
        }

        public Participant CreateParticipantMatches(int matchCount = 0, BestOf bestOf = null)
        {
            var scoringFaker = new ScoringFaker();

            var scoring = scoringFaker.FakeScoring(Game.LeagueOfLegends);

            var matchStatsFaker = new MatchStatsFaker();

            var participant = this.CreateParticipant();

            for (var index = 0; index < matchCount; index++)
            {
                participant.SnapshotMatch(new MatchReference(123123123), matchStatsFaker.FakeMatchStats(Game.LeagueOfLegends), scoring);
            }

            return participant;
        }
    }
}
