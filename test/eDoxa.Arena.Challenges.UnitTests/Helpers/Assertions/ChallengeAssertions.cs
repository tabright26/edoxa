// Filename: ChallengeAssertions.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Extensions;

using FluentAssertions;
using FluentAssertions.Primitives;

namespace eDoxa.Arena.Challenges.UnitTests.Helpers.Assertions
{
    public class ChallengeAssertions : ObjectAssertions
    {
        public ChallengeAssertions(IChallenge challenge) : base(challenge)
        {
        }

        protected override string Identifier => nameof(ChallengeAssertions);

        private IChallenge Challenge => Subject as IChallenge;

        public AndConstraint<ChallengeAssertions> BeValid(string because = "", params object[] becauseArgs)
        {
            Challenge.Game.Should().Should().NotBe(ChallengeGame.All, because, becauseArgs);

            Challenge.Game.Should().Should().NotBe(new ChallengeGame(), because, becauseArgs);

            Challenge.Timeline.State.Should().NotBe(ChallengeState.All, because, becauseArgs);

            Challenge.Timeline.State.Should().NotBe(new ChallengeState(), because, becauseArgs);

            Challenge.Participants.Should().NotBeNullOrEmpty(because, becauseArgs);

            Challenge.Participants.ForEach(
                participant =>
                {
                    Challenge.SynchronizedAt?.Should().BeAfter(participant.RegisteredAt, because, becauseArgs);

                    participant.RegisteredAt.Should().BeAfter(Challenge.Timeline.CreatedAt, because, becauseArgs);

                    if (Challenge.Timeline.State != ChallengeState.Inscription)
                    {
                        participant.Matches.Should().NotBeNullOrEmpty(because, becauseArgs);
                    }

                    participant.Matches.ForEach(
                        match =>
                        {
                            Challenge.SynchronizedAt?.Should().BeOnOrAfter(match.SynchronizedAt, because, becauseArgs);

                            participant.SynchronizedAt?.Should().BeOnOrAfter(match.SynchronizedAt, because, becauseArgs);

                            match.SynchronizedAt.Should().BeAfter(participant.RegisteredAt, because, becauseArgs);
                        }
                    );
                }
            );

            Challenge.Participants.Select(participant => participant.Id).Distinct().Should().HaveCount(Challenge.Participants.Count, because, becauseArgs);

            Challenge.Participants.Select(participant => participant.UserId).Distinct().Should().HaveCount(Challenge.Participants.Count, because, becauseArgs);

            Challenge.Participants.SelectMany(participant => participant.Matches)
                .Select(match => match.Id)
                .Distinct()
                .Should()
                .HaveCount(Challenge.Participants.SelectMany(participant => participant.Matches).Count(), because, becauseArgs);

            Challenge.Participants.SelectMany(participant => participant.Matches)
                .Select(match => match.GameReference)
                .Distinct()
                .Should()
                .HaveCount(Challenge.Participants.SelectMany(participant => participant.Matches).Count(), because, becauseArgs);

            return new AndConstraint<ChallengeAssertions>(this);
        }
    }
}
