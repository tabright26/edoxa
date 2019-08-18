// Filename: ChallengeViewModelCollectionAssertions.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Assertions.Extensions;

using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Execution;

namespace eDoxa.Arena.Challenges.UnitTests.Helpers.Assertions
{
    public class ChallengeViewModelCollectionAssertions : GenericCollectionAssertions<ChallengeViewModel>
    {
        public ChallengeViewModelCollectionAssertions(IEnumerable<ChallengeViewModel> challengeViewModels) : base(challengeViewModels)
        {
        }

        protected override string Identifier => nameof(ChallengeViewModelCollectionAssertions);

        public AndConstraint<ChallengeViewModelCollectionAssertions> BeValid(string because = "", params object[] becauseArgs)
        {
            foreach (var challengeViewModel in Subject)
            {
                using (new AssertionScope(challengeViewModel.Id.ToString()))
                {
                    challengeViewModel.Should().BeValid(because, becauseArgs);
                }
            }

            return new AndConstraint<ChallengeViewModelCollectionAssertions>(this);
        }
    }
}
