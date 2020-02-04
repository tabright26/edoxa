// Filename: PaypalOptionsExtensions.cs
// Date Created: 2020-02-04
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;

using PayPal.Api;

namespace eDoxa.Paypal.Extensions
{
    internal static class PaypalOptionsExtensions
    {
        public static APIContext GetApiContext(this PaypalOptions options)
        {
            var credential = new OAuthTokenCredential(
                options.ClientId,
                options.ClientSecret,
                new Dictionary<string, string>
                {
                    ["mode"] = options.Mode
                });

            return new APIContext(credential.GetAccessToken());
        }
    }
}
