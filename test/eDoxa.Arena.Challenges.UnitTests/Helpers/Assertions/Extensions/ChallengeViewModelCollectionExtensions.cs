// Filename: ChallengeViewModelCollectionExtensions.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Api.ViewModels;

namespace eDoxa.Arena.Challenges.UnitTests.Helpers.Assertions.Extensions
{
    public static class ChallengeViewModelCollectionExtensions
    {
        public static ChallengeViewModelCollectionAssertions Should(this IEnumerable<ChallengeViewModel> challengeViewModels)
        {
            return new ChallengeViewModelCollectionAssertions(challengeViewModels);
        }
    }
}
