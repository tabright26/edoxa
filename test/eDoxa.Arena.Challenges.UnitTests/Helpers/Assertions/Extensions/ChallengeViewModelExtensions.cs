// Filename: ChallengeViewModelExtensions.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Api.ViewModels;

namespace eDoxa.Arena.Challenges.UnitTests.Helpers.Assertions.Extensions
{
    public static class ChallengeViewModelExtensions
    {
        public static ChallengeViewModelAssertions Should(this ChallengeViewModel challengeViewModel)
        {
            return new ChallengeViewModelAssertions(challengeViewModel);
        }
    }
}
