// Filename: ChallengeResponseAssertions.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Responses;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using FluentAssertions;
using FluentAssertions.Primitives;

namespace eDoxa.Arena.Challenges.UnitTests.TestHelpers.Assertions
{
    public class ChallengeResponseAssertions : ObjectAssertions
    {
        public ChallengeResponseAssertions(ChallengeResponse challengeResponse) : base(challengeResponse)
        {
        }

        protected override string Identifier => nameof(ChallengeResponseAssertions);

        private ChallengeResponse ChallengeResponse => Subject as ChallengeResponse;

        public AndConstraint<ChallengeResponseAssertions> BeValid(string because = "", params object[] becauseArgs)
        {
            ChallengeResponse.Should().NotBeNull(because, becauseArgs);

            ChallengeResponse.Id.Should().NotBeEmpty(because, becauseArgs);

            ChallengeResponse.Name.Should().NotBeNullOrWhiteSpace(because, becauseArgs);

            ChallengeResponse.Game.Should().NotBeNullOrWhiteSpace(because, becauseArgs);

            ChallengeResponse.Game.Should().NotBe(new ChallengeGame().Name, because, becauseArgs);

            ChallengeResponse.Game.Should().NotBe(ChallengeGame.All.Name, because, becauseArgs);

            ChallengeResponse.State.Should().NotBeNullOrWhiteSpace(because, becauseArgs);

            ChallengeResponse.State.Should().NotBe(new ChallengeState().Name, because, becauseArgs);

            ChallengeResponse.State.Should().NotBe(ChallengeState.All.Name, because, becauseArgs);

            ChallengeResponse.Should().NotBeNull(because, becauseArgs);

            ChallengeResponse.Timeline.Should().NotBeNull(because, becauseArgs);

            ChallengeResponse.Timeline.CreatedAt.Should().BeBefore(DateTime.UtcNow, because, becauseArgs);

            ChallengeResponse.Scoring.Should().NotBeNull(because, becauseArgs);

            ChallengeResponse.Scoring.Should().NotBeEmpty(because, becauseArgs);

            foreach (var response in ChallengeResponse.Participants)
            {
                response.Id.Should().NotBeEmpty(because, becauseArgs);

                response.UserId.Should().NotBeEmpty(because, becauseArgs);

                foreach (var matchResponse in response.Matches)
                {
                    matchResponse.Id.Should().NotBeEmpty(because, becauseArgs);

                    matchResponse.Score.Should().BeGreaterOrEqualTo(decimal.Zero, because, becauseArgs);

                    foreach (var statResponse in matchResponse.Stats)
                    {
                        statResponse.Name.Should().NotBeNullOrWhiteSpace(because, becauseArgs);
                    }
                }
            }

            return new AndConstraint<ChallengeResponseAssertions>(this);
        }
    }
}
