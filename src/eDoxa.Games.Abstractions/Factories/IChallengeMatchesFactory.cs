// Filename: IChallengeMatchesFactory.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.Abstractions.Adapter;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Games.Abstractions.Factories
{
    public interface IChallengeMatchesFactory
    {
        IChallengeMatchesAdapter CreateInstance(Game game);
    }
}
