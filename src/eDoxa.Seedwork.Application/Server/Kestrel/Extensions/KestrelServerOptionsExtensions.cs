// Filename: KestrelServerOptionsExtensions.cs
// Date Created: 2019-12-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;

using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace eDoxa.Seedwork.Application.Server.Kestrel.Extensions
{
    public static class KestrelServerOptionsExtensions
    {
        public static void ListenRest(this KestrelServerOptions options, int port = 80)
        {
            options.Listen(IPAddress.Any, port, listenOptions => listenOptions.Protocols = HttpProtocols.Http1AndHttp2);
        }

        public static void ListenGrpc(this KestrelServerOptions options, int port = 81)
        {
            options.Listen(IPAddress.Any, port, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
        }
    }
}
