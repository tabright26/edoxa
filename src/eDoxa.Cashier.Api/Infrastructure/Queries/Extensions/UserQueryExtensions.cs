// Filename: UserQueryExtensions.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Cashier.Api.Infrastructure.Queries.Extensions
{
    public static class UserQueryExtensions
    {
        public static async Task<UserModel> FindUserModelAsync(this IUserQuery userQuery, UserId userId)
        {
            var user = await userQuery.FindUserAsync(userId);

            return userQuery.Mapper.Map<UserModel>(user);
        }
    }
}
