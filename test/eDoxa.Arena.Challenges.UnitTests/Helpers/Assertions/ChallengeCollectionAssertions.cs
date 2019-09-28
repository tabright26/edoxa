// Filename: ChallengeCollectionAssertions.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Assertions.Extensions;

using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Execution;

namespace eDoxa.Arena.Challenges.UnitTests.Helpers.Assertions
{
    public class ChallengeCollectionAssertions : GenericCollectionAssertions<IChallenge>
    {
        public ChallengeCollectionAssertions(IEnumerable<IChallenge> challenges) : base(challenges)
        {
        }

        protected override string Identifier => nameof(ChallengeCollectionAssertions);

        public AndConstraint<ChallengeCollectionAssertions> BeValid(string because = "", params object[] becauseArgs)
        {
            foreach (var challenge in Subject)
            {
                using (new AssertionScope(challenge.Id.ToString()))
                {
                    challenge.Should().BeValid(because, becauseArgs);
                }
            }

            return new AndConstraint<ChallengeCollectionAssertions>(this);
        }
    }
}
