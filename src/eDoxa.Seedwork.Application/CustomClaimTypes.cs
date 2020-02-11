// Filename: CustomClaimTypes.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Seedwork.Application
{
    public static class CustomClaimTypes
    {
        // Identity
        public static readonly string SecurityStamp = "security_stamp";
        public static readonly string Permission = "permission";
        public static readonly string Country = "country";
        public static readonly string Doxatag = "doxatag";

        // Clans
        public static readonly string Clan = "clan";

        // Payment
        public static readonly string StripeCustomer = "stripe:customer";

        // Games
        public static string GetGamePlayerFor(Game game)
        {
            return $"games:{game.CamelCaseName}";
        }
    }
}
