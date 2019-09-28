// Filename: IChallengeFakerFactory.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers
{
    public interface IChallengeFakerFactory
    {
        IChallengeFaker CreateFaker(int? seed);
    }
}
