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
        public static readonly Scope IdentityApi = new Scope("identity.api", "eDoxa Identity API");
        public static readonly Scope CashierApi = new Scope("cashier.api", "eDoxa Cashier API");
        public static readonly Scope PaymentApi = new Scope("payment.api", "eDoxa Payment API");
        public static readonly Scope NotificationsApi = new Scope("notifications.api", "eDoxa Notifications API");
        public static readonly Scope GamesApi = new Scope("games.api", "eDoxa Games API");
        public static readonly Scope ClansApi = new Scope("clans.api", "eDoxa Clans API");
        public static readonly Scope ChallengesApi = new Scope("challenges.api", "eDoxa Challenges API");
        public static readonly Scope ChallengesAggregator = new Scope("challenges.aggregator", "eDoxa Chalenges Aggregator");
    }
}
