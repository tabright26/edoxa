// Filename: HttpContextAccessorExtensions.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;
using eDoxa.Seedwork.Security.Extensions;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Api.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static Func<Game, ExternalAccount> FuncExternalAccount(this IHttpContextAccessor accessor)
        {
            return game =>
            {
                var externalAccount = accessor.GetClaimOrDefault(game.GetClaimType());

                return externalAccount != null ? new ExternalAccount(externalAccount) : null;
            };
        }
    }
}
