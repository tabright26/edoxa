// Filename: TransactionFakerFactory.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;

namespace eDoxa.Cashier.UnitTests.Helpers
{
    public sealed class TransactionFakerFactory : ITransactionFakerFactory
    {
        public ITransactionFaker CreateFaker(int? seed)
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
