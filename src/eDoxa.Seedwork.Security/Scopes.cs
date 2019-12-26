// Filename: Scopes.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Security
{
    public static class Scopes
    {
        public static readonly Scope Country = new Scope("country");
        public static readonly Scope Roles = new Scope("roles");
        public static readonly Scope Permissions = new Scope("permissions");
        public static readonly Scope Games = new Scope("games");
        public static readonly Scope IdentityApi = new Scope("identity.api", "Identity API");
        public static readonly Scope CashierApi = new Scope("cashier.api", "Cashier API");
        public static readonly Scope CashierWebAggregator = new Scope("cashier.web.aggregator", "Cashier Web Aggregator");
        public static readonly Scope PaymentApi = new Scope("payment.api", "Payment API");
        public static readonly Scope NotificationsApi = new Scope("notifications.api", "Notifications API");
        public static readonly Scope GamesApi = new Scope("games.api", "Games API");
        public static readonly Scope ClansApi = new Scope("clans.api", "Clans API");
        public static readonly Scope ChallengesApi = new Scope("challenges.api", "Challenges API");
        public static readonly Scope ChallengesWebAggregator = new Scope("challenges.web.aggregator", "Challenges Web Aggregator");
    }
}
