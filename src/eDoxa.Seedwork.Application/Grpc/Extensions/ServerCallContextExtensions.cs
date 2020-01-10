// Filename: ServerCallContextExtensions.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Grpc.Extensions;
using eDoxa.Seedwork.Domain;

using Grpc.Core;

namespace eDoxa.Seedwork.Application.Grpc.Extensions
{
    public static class ServerCallContextExtensions
    {
        internal const string Errors = "errors";

        private const string Detail = "Please refer to the errors property for additional details.";

        public static RpcException InvalidArgumentRpcException(this ServerCallContext context, IDomainValidationResult result)
        {
            var status = new Status(StatusCode.InvalidArgument, Detail);

            var trailers = new Metadata
            {
                {Errors, result.ToJsonErrors()}
            };

            return context.RpcException(status, trailers);
        }

        public static RpcException FailedPreconditionRpcException(this ServerCallContext context, IDomainValidationResult result)
        {
            var status = new Status(StatusCode.FailedPrecondition, Detail);

            var trailers = new Metadata
            {
                {Errors, result.ToJsonErrors()}
            };

            return context.RpcException(status, trailers);
        }
    }
}
