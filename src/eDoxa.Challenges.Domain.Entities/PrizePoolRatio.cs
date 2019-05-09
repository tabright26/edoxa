// Filename: PrizePoolRatio.cs
// Date Created: 2019-04-20
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
    public partial class PrizePoolRatio
    {
        internal const float Min = 0.05F;
        internal const float Max = 0.25F;
        internal const float Default = 0.1F;

        internal static readonly PrizePoolRatio MinValue = new PrizePoolRatio(Min);
        internal static readonly PrizePoolRatio MaxValue = new PrizePoolRatio(Max);
        internal static readonly PrizePoolRatio DefaultValue = new PrizePoolRatio(Default);

        private readonly float _value;

        public PrizePoolRatio(float prizePoolRatio)
        {
            if (prizePoolRatio < Min ||
                prizePoolRatio > Max)
            {
                throw new ArgumentException(nameof(prizePoolRatio));
            }

            _value = prizePoolRatio;
        }

        public static implicit operator float(PrizePoolRatio prizePoolRatio)
        {
            return prizePoolRatio._value;
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }
    }

    public partial class PrizePoolRatio : IEquatable<PrizePoolRatio>
    {
        public bool Equals(PrizePoolRatio other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PrizePoolRatio);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class PrizePoolRatio : IComparable, IComparable<PrizePoolRatio>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as PrizePoolRatio);
        }

        public int CompareTo([CanBeNull] PrizePoolRatio other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}