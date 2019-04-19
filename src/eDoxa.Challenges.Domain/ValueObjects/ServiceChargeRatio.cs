﻿// Filename: ServiceChargeRatio.cs
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

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.ValueObjects
{
    public partial class ServiceChargeRatio : ValueObject
    {
        internal const float MinServiceChargeRatio = 0.1F;
        internal const float MaxServiceChargeRatio = 0.3F;
        internal const float DefaultPrimitive = 0.2F;

        public static readonly ServiceChargeRatio Default = new ServiceChargeRatio(DefaultPrimitive);

        private readonly float _serviceChargeRatio;

        public ServiceChargeRatio(float serviceChargeRatio, bool validate = true)
        {
            if (validate)
            {
                if (serviceChargeRatio < MinServiceChargeRatio ||
                    serviceChargeRatio > MaxServiceChargeRatio ||
                    (decimal) serviceChargeRatio % 0.01M != 0)
                {
                    throw new ArgumentException(nameof(serviceChargeRatio));
                }
            }

            _serviceChargeRatio = serviceChargeRatio;
        }

        public float ToSingle()
        {
            return _serviceChargeRatio;
        }
    }

    public partial class ServiceChargeRatio : IComparable, IComparable<ServiceChargeRatio>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as ServiceChargeRatio);
        }

        public int CompareTo([CanBeNull] ServiceChargeRatio other)
        {
            return _serviceChargeRatio.CompareTo(other?._serviceChargeRatio);
        }
    }
}