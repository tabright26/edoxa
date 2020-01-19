// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-12-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Grpc.AspNetCore.Server;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.Grpc.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomGrpc(this IServiceCollection services, Action<GrpcServiceOptions>? action = null)
        {
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;

                action?.Invoke(options);
            });
        }
    }
}
