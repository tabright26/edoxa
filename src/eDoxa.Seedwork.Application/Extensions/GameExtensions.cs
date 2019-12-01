// Filename: GameExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Security;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class GameExtensions
    {
        public static Claim ToClaim(this Game game, PlayerId playerId)
        {
            return new Claim($"games/{game.NormalizedName}", playerId);
        }
    }
}
