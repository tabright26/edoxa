// Filename: IChallengeMatchesFactory.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.Domain.Adapters;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Domain.Factories
{
    public interface IChallengeMatchesFactory
    {
        IChallengeMatchesAdapter CreateInstance(Game game);
    }
}
