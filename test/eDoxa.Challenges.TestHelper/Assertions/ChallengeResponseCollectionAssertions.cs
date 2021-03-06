﻿// Filename: ChallengeResponseCollectionAssertions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Challenges.TestHelper.Assertions.Extensions;
using eDoxa.Grpc.Protos.Challenges.Dtos;

using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Execution;

namespace eDoxa.Challenges.TestHelper.Assertions
{
    public class ChallengeResponseCollectionAssertions : GenericCollectionAssertions<ChallengeDto>
    {
        public ChallengeResponseCollectionAssertions(IEnumerable<ChallengeDto> challengeResponses) : base(challengeResponses)
        {
        }

        protected override string Identifier => nameof(ChallengeResponseCollectionAssertions);

        public AndConstraint<ChallengeResponseCollectionAssertions> BeValid(string because = "", params object[] becauseArgs)
        {
            foreach (var challengeResponse in Subject)
            {
                using (new AssertionScope(challengeResponse.Id))
                {
                    challengeResponse.Should().BeValid(because, becauseArgs);
                }
            }

            return new AndConstraint<ChallengeResponseCollectionAssertions>(this);
        }
    }
}
