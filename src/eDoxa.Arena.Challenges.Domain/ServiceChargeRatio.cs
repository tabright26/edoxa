// Filename: ServiceChargeRatio.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Globalization;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    public class ServiceChargeRatio : TypeObject<ServiceChargeRatio, float>
    {
        public const float Min = 0.1F;
        public const float Max = 0.3F;
        public const float Default = 0.2F;

        public static readonly ServiceChargeRatio MinValue = new ServiceChargeRatio(Min);
        public static readonly ServiceChargeRatio MaxValue = new ServiceChargeRatio(Max);
        public static readonly ServiceChargeRatio DefaultValue = new ServiceChargeRatio(Default);

        public ServiceChargeRatio(float serviceChargeRatio, bool validate = true) : base(serviceChargeRatio)
        {
            if (validate)
            {
                if (serviceChargeRatio < Min || serviceChargeRatio > Max || (decimal) serviceChargeRatio % 0.01M != 0)
                {
                    throw new ArgumentException(nameof(serviceChargeRatio));
                }
            }
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
