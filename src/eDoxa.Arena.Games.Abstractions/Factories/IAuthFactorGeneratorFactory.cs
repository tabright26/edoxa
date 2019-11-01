// Filename: IAuthFactorGeneratorFactory.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Games.Abstractions.Adapter;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Games.Abstractions.Factories
{
    public interface IAuthFactorGeneratorFactory
    {
        IAuthFactorGeneratorAdapter CreateInstance(Game game);
    }
}
