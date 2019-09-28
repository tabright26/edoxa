// Filename: ChallengeFakerFactory.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers
{
    public sealed class ChallengeFakerFactory : IChallengeFakerFactory
    {
        public IChallengeFaker CreateFaker(int? seed)
        {
            var faker = new ChallengeFaker();

            if (seed.HasValue)
            {
                faker.UseSeed(seed.Value);
            }

            return faker;
        }
    }
}
