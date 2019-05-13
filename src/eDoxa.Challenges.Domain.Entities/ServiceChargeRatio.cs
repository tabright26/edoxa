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
using System.Globalization;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.Entities
{
    public partial class ServiceChargeRatio
    {
        public const float Min = 0.1F;
        public const float Max = 0.3F;
        public const float Default = 0.2F;

        public static readonly ServiceChargeRatio MinValue = new ServiceChargeRatio(Min);
        public static readonly ServiceChargeRatio MaxValue = new ServiceChargeRatio(Max);
        public static readonly ServiceChargeRatio DefaultValue = new ServiceChargeRatio(Default);

        private readonly float _value;

        public ServiceChargeRatio(float serviceChargeRatio, bool validate = true)
        {
            if (validate)
            {
                if (serviceChargeRatio < Min ||
                    serviceChargeRatio > Max ||
                    (decimal) serviceChargeRatio % 0.01M != 0)
                {
                    throw new ArgumentException(nameof(serviceChargeRatio));
                }
            }

            _value = serviceChargeRatio;
        }

        public static implicit operator float(ServiceChargeRatio serviceChargeRatio)
        {
            return serviceChargeRatio._value;
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }
    }

    public partial class ServiceChargeRatio : IEquatable<ServiceChargeRatio>
    {
        public bool Equals(ServiceChargeRatio other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ServiceChargeRatio);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
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
            return _value.CompareTo(other?._value);
        }
    }
}