// Filename: ServiceChargeRatio.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.ValueObjects
{
    public class ServiceChargeRatio : ValueObject
    {
        internal const float MinServiceChargeRatio = 0.1F;
        internal const float MaxServiceChargeRatio = 0.3F;

        public static readonly ServiceChargeRatio Default = new ServiceChargeRatio(0.2F);

        private readonly float _serviceChargeRatio;

        public ServiceChargeRatio(float serviceChargeRatio)
        {
            if (serviceChargeRatio < MinServiceChargeRatio ||
                serviceChargeRatio > MaxServiceChargeRatio ||
                (decimal) serviceChargeRatio % 0.01M != 0)
            {
                throw new ArgumentException(nameof(serviceChargeRatio));
            }

            _serviceChargeRatio = serviceChargeRatio;
        }

        public float ToSingle()
        {
            return _serviceChargeRatio;
        }
    }
}