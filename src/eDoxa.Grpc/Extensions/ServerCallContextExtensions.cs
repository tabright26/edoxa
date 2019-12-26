// Filename: ServerCallContextExtensions.cs
// Date Created: 2019-12-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Grpc.Core;

using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace eDoxa.Grpc.Extensions
{
    public static class ServerCallContextExtensions
    {
        public static TResponse Ok<TResponse>(this ServerCallContext context, TResponse response, string? detail = null)
        {
            var logger = context.GetLogger();

            context.Status = new Status(StatusCode.OK, detail ?? string.Empty);

            logger.Information(detail);

            return response;
        }

        public static TResponse AlreadyExists<TResponse>(this ServerCallContext context, TResponse response, string? detail = null)
        {
            var logger = context.GetLogger();

            context.Status = new Status(StatusCode.AlreadyExists, detail ?? string.Empty);

            logger.Warning(detail);

            return response;
        }

        private static ILogger GetLogger(this ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();

            return httpContext.RequestServices.GetRequiredService<ILogger>();
        }

        public static RpcException NotFoundRpcException(this ServerCallContext context, string detail)
        {
            return context.RpcException(new Status(StatusCode.NotFound, detail));
        }

        public static RpcException RpcException(this ServerCallContext context, Status status)
        {
            var logger = context.GetLogger();

            var exception = context.ResponseTrailers == null ? new RpcException(status) : new RpcException(status, context.ResponseTrailers);

            switch (status.StatusCode)
            {
                case StatusCode.NotFound:
                case StatusCode.Cancelled:
                case StatusCode.Aborted:
                case StatusCode.Unauthenticated:
                case StatusCode.PermissionDenied:
                {
                    logger.Warning(exception, status.Detail);

                    break;
                }

                case StatusCode.InvalidArgument:
                case StatusCode.DeadlineExceeded:
                case StatusCode.FailedPrecondition:
                case StatusCode.OutOfRange:
                {
                    logger.Error(exception, status.Detail);

                    break;
                }

                case StatusCode.Unimplemented:
                case StatusCode.Internal:
                case StatusCode.Unavailable:
                case StatusCode.DataLoss:
                case StatusCode.Unknown:
                case StatusCode.ResourceExhausted:
                {
                    logger.Fatal(exception, status.Detail);

                    break;
                }

                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            return exception;
        }
    }
}
