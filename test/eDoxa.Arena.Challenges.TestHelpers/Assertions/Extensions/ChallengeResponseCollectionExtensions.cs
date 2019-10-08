﻿// Filename: ChallengeResponseCollectionExtensions.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Responses;

namespace eDoxa.Arena.Challenges.TestHelpers.Assertions.Extensions
{
    public static class ChallengeResponseCollectionExtensions
    {
        public static ChallengeResponseCollectionAssertions Should(this IEnumerable<ChallengeResponse> responses)
        {
            return new ChallengeResponseCollectionAssertions(responses);
        }
    }
}