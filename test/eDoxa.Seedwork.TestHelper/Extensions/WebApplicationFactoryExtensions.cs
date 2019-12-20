// Filename: WebApplicationFactoryExtensions.cs
// Date Created: 2019-12-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.TestHelper.DelegatingHandlers;

using Grpc.Net.Client;

using Microsoft.AspNetCore.Mvc.Testing;

namespace eDoxa.Seedwork.TestHelper.Extensions
{
    public static class WebApplicationFactoryExtensions
    {
        public static GrpcChannel CreateChannel<TStartup>(this WebApplicationFactory<TStartup> factory)
        where TStartup : class
        {
            var httpClient = factory.CreateDefaultClient(
                new VersionDelegatingHandler
                {
                    InnerHandler = factory.Server.CreateHandler()
                });

            return GrpcChannel.ForAddress(
                httpClient.BaseAddress,
                new GrpcChannelOptions
                {
                    HttpClient = httpClient
                });
        }
    }
}
