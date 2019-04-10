// Filename: ClientStoreExtensions.cs
// Date Created: 2019-04-01
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using IdentityServer4.Stores;

namespace eDoxa.Identity.Extensions
{
    public static class ClientStoreExtensions
    {
        public static async Task<bool> IsPkceClientAsync(this IClientStore store, string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                return false;
            }

            var client = await store.FindEnabledClientByIdAsync(clientId);

            return client?.RequirePkce == true;
        }
    }
}