// Filename: TransferFaker.cs
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
    public sealed class TransferFaker : Faker<Transfer>
    {
        public TransferFaker()
        {
            this.UseSeed(8675309);
        }

        public Transfer FakeTransfer()
        {
            return this.Generate();
        }
    }
}
