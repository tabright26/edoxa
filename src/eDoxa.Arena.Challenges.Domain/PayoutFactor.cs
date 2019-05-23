// Filename: PayoutFactor.cs
// Date Created: 2019-05-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    public abstract class PayoutFactor : TypeObject<PayoutFactor, int>
    {
        protected PayoutFactor(int value) : base(value)
        {
        }
    }
}
