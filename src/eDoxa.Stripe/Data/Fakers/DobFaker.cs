// Filename: DobFaker.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using Stripe;

namespace eDoxa.Stripe.Data.Fakers
{
    public sealed class DobFaker : Faker<Dob>
    {
        public DobFaker()
        {
            this.UseSeed(8675309);

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
    }
}
