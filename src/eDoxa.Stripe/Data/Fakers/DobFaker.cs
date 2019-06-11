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

using eDoxa.Seedwork.Common.Abstactions;

using Stripe;

namespace eDoxa.Stripe.Data.Fakers
{
    public sealed class DobFaker : CustomFaker<Dob>
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
