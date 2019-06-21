// Filename: HttpContextAccessorExtensions.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Security.Extensions;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Api.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static Func<ChallengeGame, GameAccountId> FuncUserGameReference(this IHttpContextAccessor accessor)
        {
            return game =>
            {
                var userGameReference = accessor.GetClaimOrDefault(game.GetClaimType());

                return userGameReference != null ? new GameAccountId(userGameReference) : null;
            };
        }
    }
}
