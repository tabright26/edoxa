// Filename: Scopes.cs
// Date Created: 2019-09-02
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
        public static readonly Scope IdentityApi = new Scope("identity.api");
        public static readonly Scope CashierApi =new Scope("cashier.api");
        public static readonly Scope PaymentApi = new Scope("payment.api");
        public static readonly Scope NotificationsApi = new Scope("notifications.api");
        public static readonly Scope ArenaChallengesApi = new Scope("arena.challenges.api");
        public static readonly Scope ArenaGamesApi =new Scope("arena.games.api");
        public static readonly Scope ArenaGamesLeagueOfLegendsApi = new Scope("arena.games.leagueoflegends.api");
        public static readonly Scope OrganizationsClansApi =new Scope("organizations.clans.api");
    }
}
