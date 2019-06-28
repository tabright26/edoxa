// Filename: GameExtensions.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Security.Constants;

namespace eDoxa.Seedwork.Common.Extensions
{
    public static class GameExtensions
    {
        public static string GetClaimType(this Game game)
        {
            return $"{CustomClaimTypes.Game}:{game.ToString().ToLowerInvariant()}";
        }
    }
}
