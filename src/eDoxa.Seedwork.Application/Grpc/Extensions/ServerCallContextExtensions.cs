// Filename: ServerCallContextExtensions.cs
// Date Created: 2019-12-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Grpc.Extensions;
using eDoxa.Seedwork.Domain;

using Grpc.Core;

namespace eDoxa.Seedwork.Application.Grpc.Extensions
{
    public static class ServerCallContextExtensions
    {
        public static RpcException FailedPreconditionRpcException(this ServerCallContext context, IDomainValidationResult result, string detail)
        {
            context.AddDomainValidationResultToResponseTrailers(result);

            return context.RpcException(new Status(StatusCode.FailedPrecondition, detail));
        }

        private static void AddDomainValidationResultToResponseTrailers(this ServerCallContext context, IDomainValidationResult result)
        {
            context.ResponseTrailers.Add(nameof(DomainValidationResult), result.Errors.ToString());
        }
    }
}
