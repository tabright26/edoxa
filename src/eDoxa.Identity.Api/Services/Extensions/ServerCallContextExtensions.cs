// Filename: ServerCallContextExtensions.cs
// Date Created: 2019-12-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Grpc.Core;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Services.Extensions
{
    public static class ServerCallContextExtensions
    {
        public static void AddIdentityResultToResponseTrailers(this ServerCallContext context, IdentityResult result)
        {
            context.ResponseTrailers.Add(nameof(IdentityResult), result.ToString());
        }
    }
}
