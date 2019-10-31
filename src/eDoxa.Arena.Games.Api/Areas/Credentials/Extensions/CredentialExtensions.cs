// Filename: CredentialExtensions.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Games.Domain.AggregateModels.CredentialAggregate;
using eDoxa.Seedwork.Security;

namespace eDoxa.Arena.Games.Api.Areas.Credentials.Extensions
{
    public static class CredentialExtensions
    {
        public static Claim ToClaim(this Credential credential)
        {
            return new Claim($"games/{credential.Game.NormalizedName}", credential.PlayerId);
        }
    }
}
