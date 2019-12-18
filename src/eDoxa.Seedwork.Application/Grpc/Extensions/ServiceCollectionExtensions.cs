// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-12-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.Grpc.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomGrpc(this IServiceCollection services)
        {
            return services.AddGrpc(options => options.EnableDetailedErrors = true).Services;
        }
    }
}
