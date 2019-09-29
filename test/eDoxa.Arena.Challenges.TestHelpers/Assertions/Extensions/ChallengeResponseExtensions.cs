// Filename: ChallengeResponseExtensions.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Responses;

namespace eDoxa.Arena.Challenges.TestHelpers.Assertions.Extensions
{
    public static class ChallengeResponseExtensions
    {
        public static ChallengeResponseAssertions Should(this ChallengeResponse response)
        {
            return new ChallengeResponseAssertions(response);
        }
    }
}
