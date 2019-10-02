// Filename: AccountFakerFactory.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers.Abstractions;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers.Factories
{
    public sealed class FakerFactory
    {
        public IAccountFaker CreateAccountFaker(int? seed)
        {
            var faker = new AccountFaker();

            if (seed.HasValue)
            {
                faker.UseSeed(seed.Value);
            }

            return faker;
        }

        public IChallengeFaker CreateChallengeFaker(int? seed)
        {
            var faker = new ChallengeFaker();

            if (seed.HasValue)
            {
                faker.UseSeed(seed.Value);
            }

            return faker;
        }

        public ITransactionFaker CreateTransactionFaker(int? seed)
        {
            var faker = new TransactionFaker();

            if (seed.HasValue)
            {
                faker.UseSeed(seed.Value);
            }

            return faker;
        }
    }
}
