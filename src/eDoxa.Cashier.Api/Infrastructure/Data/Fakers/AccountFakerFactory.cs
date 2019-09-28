// Filename: AccountFakerFactory.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers
{
    public sealed class AccountFakerFactory : IAccountFakerFactory
    {
        public IAccountFaker CreateFaker(int? seed)
        {
            var faker = new AccountFaker();

            if (seed.HasValue)
            {
                faker.UseSeed(seed.Value);
            }

            return faker;
        }
    }
}
