// Filename: ChallengeResponseCollectionAssertions.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Responses;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Assertions.Extensions;

using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Execution;

namespace eDoxa.Arena.Challenges.UnitTests.Helpers.Assertions
{
    public class ChallengeResponseCollectionAssertions : GenericCollectionAssertions<ChallengeResponse>
    {
        public ChallengeResponseCollectionAssertions(IEnumerable<ChallengeResponse> challengeResponses) : base(challengeResponses)
        {
        }

        protected override string Identifier => nameof(ChallengeResponseCollectionAssertions);

        public AndConstraint<ChallengeResponseCollectionAssertions> BeValid(string because = "", params object[] becauseArgs)
        {
            foreach (var challengeResponse in Subject)
            {
                using (new AssertionScope(challengeResponse.Id.ToString()))
                {
                    challengeResponse.Should().BeValid(because, becauseArgs);
                }
            }

            return new AndConstraint<ChallengeResponseCollectionAssertions>(this);
        }
    }
}
