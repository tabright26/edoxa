// Filename: UserGameReferenceFaker.cs
// Date Created: 2019-06-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Seedwork.Common.Fakers
{
    public sealed class UserGameReferenceFaker : CustomFaker<UserGameReference>
    {
        public UserGameReferenceFaker()
        {
            this.RuleSet(
                Game.LeagueOfLegends.ToString(),
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(
                        faker => new UserGameReference($"LeagueOfLegends{faker.Random.Guid().ToString().Replace("-", string.Empty).Substring(10)}")
                    );
                }
            );
        }

        public UserGameReference FakeUserGameReference(Game game)
        {
            return this.Generate(game.ToString());
        }
    }
}
