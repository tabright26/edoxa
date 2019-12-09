// Filename: KestrelServerOptionsExtensions.cs
// Date Created: 2019-12-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;

using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace eDoxa.Seedwork.Security.Kestrel.Extensions
{
    public static class KestrelServerOptionsExtensions
    {
        public static void ListenRest(this KestrelServerOptions options)
        {
            options.Listen(IPAddress.Any, 80, listenOptions => listenOptions.Protocols = HttpProtocols.Http1AndHttp2);
        }

        public static void ListenGrpc(this KestrelServerOptions options)
        {
            options.Listen(IPAddress.Any, 81, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
        }
    }
}
