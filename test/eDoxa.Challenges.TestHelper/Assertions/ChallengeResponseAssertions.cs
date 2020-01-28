// Filename: ChallengeResponseAssertions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Challenges.Dtos;

using FluentAssertions;
using FluentAssertions.Primitives;

namespace eDoxa.Challenges.TestHelper.Assertions
{
    public class ChallengeResponseAssertions : ObjectAssertions
    {
        public ChallengeResponseAssertions(ChallengeDto challengeResponse) : base(challengeResponse)
        {
        }

        protected override string Identifier => nameof(ChallengeResponseAssertions);

#pragma warning disable CS8603 // Possible null reference return.
        private ChallengeDto ChallengeResponse => Subject as ChallengeDto;
#pragma warning restore CS8603 // Possible null reference return.

        public AndConstraint<ChallengeResponseAssertions> BeValid(string because = "", params object[] becauseArgs)
        {
            ChallengeResponse.Should().NotBeNull(because, becauseArgs);

            ChallengeResponse.Id.Should().NotBeEmpty(because, becauseArgs);

            ChallengeResponse.Name.Should().NotBeNullOrWhiteSpace(because, becauseArgs);

            ChallengeResponse.Should().NotBeNull(because, becauseArgs);

            ChallengeResponse.Timeline.Should().NotBeNull(because, becauseArgs);

            ChallengeResponse.Scoring.Should().NotBeNull(because, becauseArgs);

            ChallengeResponse.Scoring.Should().NotBeEmpty(because, becauseArgs);

            foreach (var response in ChallengeResponse.Participants)
            {
                response.Id.Should().NotBeEmpty(because, becauseArgs);

                response.UserId.Should().NotBeEmpty(because, becauseArgs);

                foreach (var matchResponse in response.Matches)
                {
                    matchResponse.Id.Should().NotBeEmpty(because, becauseArgs);

                    //matchResponse.Score.Should().BeGreaterOrEqualTo(decimal.Zero, because, becauseArgs);

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
