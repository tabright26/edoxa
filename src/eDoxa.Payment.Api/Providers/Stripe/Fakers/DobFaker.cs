﻿// Filename: DobFaker.cs
// Date Created: 2019-07-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using Stripe;

namespace eDoxa.Payment.Api.Providers.Stripe.Fakers
{
    public sealed class DobFaker : Faker<Dob>
    {
        public DobFaker()
        {
            this.CustomInstantiator(
                faker =>
                {
                    var date = faker.Date.Past(18);

                    return new Dob
                    {
                        Year = date.Year,
                        Month = date.Month,
                        Day = date.Day
                    };
                }
            );
        }

        public Dob FakeDob()
        {
            return this.Generate();
        }
    }
}
