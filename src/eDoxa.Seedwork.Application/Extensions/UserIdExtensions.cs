// Filename: UserIdExtensions.cs
// Date Created: 2019-10-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Security;

using IdentityModel;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class UserIdExtensions
    {
        public static Claim ToClaim(this UserId userId)
        {
            return new Claim(JwtClaimTypes.Subject, userId.ToString());
        }
    }
}
