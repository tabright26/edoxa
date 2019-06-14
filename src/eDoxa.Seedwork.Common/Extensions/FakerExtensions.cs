// Filename: FakerExtensions.cs
// Date Created: 2019-06-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Bogus;

using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Seedwork.Common.Extensions
{
    public static class FakerExtensions
    {
        public static UserId UserId(this Faker faker)
        {
            return ValueObjects.UserId.FromGuid(faker.Random.Guid());
        }

        public static UserGameReference UserGameReference(this Faker faker, Game game)
        {
            if (game == Game.LeagueOfLegends)
            {
                return new UserGameReference(faker.Random.Replace("*****_*************************"));
            }

            throw new ArgumentNullException(nameof(game));
        }
    }
}
