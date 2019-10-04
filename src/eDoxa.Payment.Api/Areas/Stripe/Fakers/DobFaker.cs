// Filename: DobFaker.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Fakers
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
                });
        }

        public Dob FakeDob()
        {
            return this.Generate();
        }
    }
}
