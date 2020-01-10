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

        public static RpcException InvalidArgumentRpcException(this ServerCallContext context, IDomainValidationResult result)
        {
            var trailers = new Metadata
            {
                {Errors, result.ToJsonErrors()}
            };

            var status = new Status(StatusCode.InvalidArgument, "The request has invalid argument(s).");

            return context.RpcException(status, trailers);
        }

        public static RpcException FailedPreconditionRpcException(this ServerCallContext context, IDomainValidationResult result, string detail)
        {
            var trailers = new Metadata
            {
                {Errors, result.ToJsonErrors()}
            };

            var status = new Status(StatusCode.FailedPrecondition, detail);

            return context.RpcException(status, trailers);
        }
    }
}
