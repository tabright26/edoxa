// Filename: ProblemDetailsOptionsExtensions.cs
// Date Created: 2019-12-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Grpc.Core;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Application.Grpc.Extensions
{
    public static class ProblemDetailsOptionsExtensions
    {
        public static void MapRpcException(this ProblemDetailsOptions options)
        {
            options.Map<RpcException>(
                exception =>
                {
                    switch (exception.StatusCode)
                    {
                        case StatusCode.NotFound:
                        {
                            return new ExceptionProblemDetails(exception, StatusCodes.Status404NotFound);
                        }

                        case StatusCode.PermissionDenied:
                        {
                            return new ExceptionProblemDetails(exception, StatusCodes.Status403Forbidden);
                        }

                        case StatusCode.Unauthenticated:
                        {
                            return new ExceptionProblemDetails(exception, StatusCodes.Status401Unauthorized);
                        }

                        case StatusCode.InvalidArgument:
                        {
                            return new ExceptionProblemDetails(exception, StatusCodes.Status400BadRequest); // TODO: Add errros from tillers.
                        }

                        case StatusCode.FailedPrecondition:
                        {
                            return new ExceptionProblemDetails(exception, StatusCodes.Status412PreconditionFailed); // TODO: Add errros from tillers.
                        }

                        case StatusCode.Unimplemented:
                        {
                            return new ExceptionProblemDetails(exception, StatusCodes.Status501NotImplemented);
                        }

                        case StatusCode.Unavailable:
                        {
                            return new ExceptionProblemDetails(exception, StatusCodes.Status503ServiceUnavailable);
                        }

                        case StatusCode.ResourceExhausted:
                        case StatusCode.DeadlineExceeded:
                        case StatusCode.Aborted:
                        case StatusCode.OutOfRange:
                        case StatusCode.Internal:
                        case StatusCode.Cancelled:
                        case StatusCode.DataLoss:
                        case StatusCode.Unknown:
                        case StatusCode.OK:
                        case StatusCode.AlreadyExists:
                        default:
                        {
                            return new ExceptionProblemDetails(exception, StatusCodes.Status500InternalServerError);
                        }
                    }
                });
        }
    }
}
