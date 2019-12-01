// Filename: ConfigurationExtensions.cs
// Date Created: 2019-12-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace eDoxa.Seedwork.Security.ForwardedHeaders.Extensions
{
    public static class ConfigurationExtensions
    {
        public static bool IsFowardedHeadersEnabled(this IConfiguration configuration)
        {
            return configuration.GetValue<bool?>("ASPNETCORE_FORWARDEDHEADERS_ENABLED") ?? false;
        }
    }
}
