// Filename: ChallengeResponseCollectionExtensions.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Grpc.Protos.Challenges.Dtos;

namespace eDoxa.Challenges.TestHelper.Assertions.Extensions
{
    public static class ChallengeResponseCollectionExtensions
    {
        public static ChallengeResponseCollectionAssertions Should(this IEnumerable<ChallengeDto> responses)
        {
            return new ChallengeResponseCollectionAssertions(responses);
        }
    }
}
