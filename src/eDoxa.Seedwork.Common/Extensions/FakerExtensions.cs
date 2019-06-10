// Filename: FakerExtensions.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

namespace eDoxa.Seedwork.Common.Extensions
{
    public static class FakerExtensions
    {
        private const int Seed = 8675309;

        public static void UseSeed<T>(this Faker<T> faker)
        where T : class
        {
            faker.UseSeed(Seed);
        }
    }
}
