﻿// Filename: IAuthFactorGeneratorFactory.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.Domain.Adapters;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Domain.Factories
{
    public interface IGameAuthenticationGeneratorFactory
    {
        IAuthenticationGeneratorAdapter CreateInstance(Game game);
    }
}
