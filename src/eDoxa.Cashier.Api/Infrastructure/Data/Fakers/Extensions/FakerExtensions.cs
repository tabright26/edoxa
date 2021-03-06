﻿// Filename: FakerExtensions.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers.DataSets;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers.Extensions
{
    public static class FakerExtensions
    {
        public static ChallengeDataSet Challenge(this Faker faker)
        {
            return new ChallengeDataSet(faker);
        }

        public static UserDataSet User(this Faker faker)
        {
            return new UserDataSet(faker);
        }
    }
}
