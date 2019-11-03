// Filename: ChallengeResponseExtensions.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Challenges.Api.Areas.Challenges.Responses;

namespace eDoxa.Challenges.TestHelper.Assertions.Extensions
{
    public static class ChallengeResponseExtensions
    {
        public static ChallengeResponseAssertions Should(this ChallengeResponse response)
        {
            return new ChallengeResponseAssertions(response);
        }
    }
}
