// Filename: IAuthFactorGeneratorFactory.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.Abstractions.Adapter;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Games.Abstractions.Factories
{
    public interface IAuthenticationGeneratorFactory
    {
        IAuthFactorGeneratorAdapter CreateInstance(Game game);
    }
}
