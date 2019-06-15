// Filename: CustomFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

namespace eDoxa.Seedwork.Common.Abstactions
{
    public abstract class CustomFaker<T> : Faker<T>
    where T : class
    {
        private const int SeedDefault = 8675309;

        protected CustomFaker(string locale = "en", IBinder binder = null) : base(locale, binder)
        {
            this.UseSeed(SeedDefault);
        }

        protected CustomFaker(string locale) : base(locale)
        {
            this.UseSeed(SeedDefault);
        }

        protected CustomFaker()
        {
            this.UseSeed(SeedDefault);
        }
    }
}
