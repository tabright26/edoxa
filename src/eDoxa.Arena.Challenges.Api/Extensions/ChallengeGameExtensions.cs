// Filename: ChallengeGameExtensions.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Security.Extensions;

namespace eDoxa.Arena.Challenges.Api.Extensions
{
    public static class ChallengeGameExtensions
    {
        public static string GetClaimType(this ChallengeGame game)
        {
            return Game.FromValue(game.Value).GetClaimType();
        }
    }
}
