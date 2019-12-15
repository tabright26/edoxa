// Filename: ChallengeResponseExtensions.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Challenges.Dtos;

namespace eDoxa.Challenges.TestHelper.Assertions.Extensions
{
    public static class ChallengeResponseExtensions
    {
        public static ChallengeResponseAssertions Should(this ChallengeDto response)
        {
            return new ChallengeResponseAssertions(response);
        }
    }
}
