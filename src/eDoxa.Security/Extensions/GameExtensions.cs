﻿// Filename: GameExtensions.cs
// Date Created: 2019-05-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Security.Extensions
{
    public static class GameExtensions
    {
        public static string GetClaimType(this Game game)
        {
            return $"{CustomClaimTypes.Game}:{game.ToString().ToLowerInvariant()}";
        }
    }
}