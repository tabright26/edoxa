// Filename: ChallengeResponseCollectionExtensions.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Responses;

namespace eDoxa.Arena.Challenges.UnitTests.Helpers.Assertions.Extensions
{
    public static class ChallengeResponseCollectionExtensions
    {
        public static ChallengeResponseCollectionAssertions Should(this IEnumerable<ChallengeResponse> responses)
        {
            return new ChallengeResponseCollectionAssertions(responses);
        }
    }
}
