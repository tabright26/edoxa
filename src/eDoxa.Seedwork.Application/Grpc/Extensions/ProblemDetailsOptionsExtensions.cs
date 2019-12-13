using Grpc.Core;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Application.Grpc.Extensions
{
    public static class ProblemDetailsOptionsExtensions
    {
        public static void MapRpcException(this ProblemDetailsOptions options)
        {
            options.Map<RpcException>(exception => new ExceptionProblemDetails(exception, StatusCodes.Status500InternalServerError));
        }
    }
}
