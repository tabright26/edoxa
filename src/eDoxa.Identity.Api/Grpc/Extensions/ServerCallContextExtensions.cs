// Filename: ServerCallContextExtensions.cs
// Date Created: 2019-12-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Grpc.Extensions;

using Grpc.Core;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Grpc.Extensions
{
    public static class ServerCallContextExtensions
    {
        public static RpcException FailedPreconditionRpcException(this ServerCallContext context, IdentityResult result, string detail)
        {
            context.AddIdentityResultToResponseTrailers(result);

            return context.RpcException(new Status(StatusCode.FailedPrecondition, detail));
        }

        private static void AddIdentityResultToResponseTrailers(this ServerCallContext context, IdentityResult result)
        {
            context.ResponseTrailers.Add(nameof(IdentityResult), result.ToString());
        }
    }
}
