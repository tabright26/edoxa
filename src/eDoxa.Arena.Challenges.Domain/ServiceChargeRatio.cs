// Filename: ServiceChargeRatio.cs
// Date Created: 2019-05-20
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
    public class ServiceChargeRatio : TypedObject<ServiceChargeRatio, float>
    {
        private const float DefaultValue = 0.2F;

        internal static readonly ServiceChargeRatio Default = new ServiceChargeRatio(DefaultValue);

        internal ServiceChargeRatio(float serviceChargeRatio)
        {
            Value = serviceChargeRatio;
        }

        public override string ToString()
        {
            return Value.ToString("P2");
        }
    }
}
