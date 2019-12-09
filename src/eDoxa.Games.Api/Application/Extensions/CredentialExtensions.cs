// Filename: CredentialExtensions.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Security;

namespace eDoxa.Games.Api.Application.Extensions
{
    public static class CredentialExtensions
    {
        public static Claim ToClaim(this Credential credential)
        {
            return new Claim($"games/{credential.Game.NormalizedName}", credential.PlayerId);
        }
    }
}
