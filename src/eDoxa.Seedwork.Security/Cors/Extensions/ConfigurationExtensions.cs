// Filename: ConfigurationExtensions.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace eDoxa.Seedwork.Security.Cors.Extensions
{
    public static class ConfigurationExtensions
    {
        private const string ASPNETCORE_HOSTINGSTARTUP__CORS__CONFIGURATIONENABLED = "ASPNETCORE_HOSTINGSTARTUP__CORS__CONFIGURATIONENABLED";

        public static bool IsCorsEnabled(this IConfiguration configuration)
        {
            return configuration.GetValue<bool?>(ASPNETCORE_HOSTINGSTARTUP__CORS__CONFIGURATIONENABLED) ?? true;
        }
    }
}
