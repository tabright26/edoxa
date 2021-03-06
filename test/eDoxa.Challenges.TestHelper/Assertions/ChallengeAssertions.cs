﻿// Filename: ChallengeAssertions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;
using FluentAssertions.Primitives;

namespace eDoxa.Challenges.TestHelper.Assertions
{
    public class ChallengeAssertions : ObjectAssertions
    {
        public ChallengeAssertions(IChallenge challenge) : base(challenge)
        {
        }

        protected override string Identifier => nameof(ChallengeAssertions);

#pragma warning disable CS8603 // Possible null reference return.
        private IChallenge Challenge => Subject as IChallenge;
#pragma warning restore CS8603 // Possible null reference return.

        public AndConstraint<ChallengeAssertions> BeValid(string because = "", params object[] becauseArgs)
        {
            Challenge.Game.Should().Should().NotBe(Game.All, because, becauseArgs);

            Challenge.Game.Should().Should().NotBe(new Game(), because, becauseArgs);

            Challenge.Timeline.State.Should().NotBe(ChallengeState.All, because, becauseArgs);

            Challenge.Timeline.State.Should().NotBe(new ChallengeState(), because, becauseArgs);

            Challenge.Participants.Should().NotBeNullOrEmpty(because, becauseArgs);

            foreach (var participant in Challenge.Participants)
            {
                Challenge.SynchronizedAt?.Should().BeAfter(participant.RegisteredAt, because, becauseArgs);

                participant.RegisteredAt.Should().BeAfter(Challenge.Timeline.CreatedAt, because, becauseArgs);

                if (Challenge.Timeline.State != ChallengeState.Inscription)
                {
                    participant.Matches.Should().NotBeNullOrEmpty(because, becauseArgs);
                }
            }

            Challenge.Participants.Select(participant => participant.Id).Distinct().Should().HaveCount(Challenge.Participants.Count, because, becauseArgs);

            Challenge.Participants.Select(participant => participant.UserId).Distinct().Should().HaveCount(Challenge.Participants.Count, because, becauseArgs);

            Challenge.Participants.SelectMany(participant => participant.Matches)
                .Select(match => match.Id)
                .Distinct()
                .Should()
                .HaveCount(Challenge.Participants.SelectMany(participant => participant.Matches).Count(), because, becauseArgs);

            Challenge.Participants.SelectMany(participant => participant.Matches)
                .Select(match => match.GameUuid)
                .Distinct()
                .Should()
                .HaveCount(Challenge.Participants.SelectMany(participant => participant.Matches).Count(), because, becauseArgs);

            return new AndConstraint<ChallengeAssertions>(this);
        }
    }
}
