// Filename: PayoutRatio.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class PayoutRatio
    {
        internal const float Min = 0.25F;
        internal const float Max = 0.75F;
        internal const float Default = 0.5F;

        public static readonly PayoutRatio MinValue = new PayoutRatio(Min);
        public static readonly PayoutRatio MaxValue = new PayoutRatio(Max);
        public static readonly PayoutRatio DefaultValue = new PayoutRatio(Default);

        private readonly float _value;

        public PayoutRatio(float payoutRatio, bool validate = true)
        {
            if (validate)
            {
                if (payoutRatio < Min ||
                    payoutRatio > Max ||
                    (decimal) payoutRatio % 0.05M != 0)
                {
                    throw new ArgumentException(nameof(payoutRatio));
                }
            }

            _value = payoutRatio;
        }

        public static implicit operator float(PayoutRatio payoutRatio)
        {
            return payoutRatio._value;
        }

        public override string ToString()
        {
            return _value.ToString("R");
        }
    }

    public partial class PayoutRatio : IEquatable<PayoutRatio>
    {
        public bool Equals([CanBeNull] PayoutRatio other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as PayoutRatio);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class PayoutRatio : IComparable, IComparable<PayoutRatio>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as PayoutRatio);
        }

        public int CompareTo([CanBeNull] PayoutRatio other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}