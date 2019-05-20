// Filename: LinkedAccount.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate
{
    public sealed partial class LinkedAccount
    {
        private readonly string _value;

        public LinkedAccount(string linkedAccount)
        {
            if (string.IsNullOrWhiteSpace(linkedAccount) ||
                !linkedAccount.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'))
            {
                throw new ArgumentException(nameof(linkedAccount));
            }

            _value = linkedAccount;
        }

        public LinkedAccount(Guid linkedAccount)
        {
            if (linkedAccount == Guid.Empty)
            {
                throw new ArgumentException(nameof(linkedAccount));
            }

            _value = linkedAccount.ToString();
        }

        public static implicit operator LinkedAccount(string linkedAccount)
        {
            return new LinkedAccount(linkedAccount);
        }

        public static implicit operator LinkedAccount(Guid linkedAccount)
        {
            return new LinkedAccount(linkedAccount);
        }

        public override string ToString()
        {
            return _value;
        }
    }

    public partial class LinkedAccount : IEquatable<LinkedAccount>
    {
        public bool Equals(LinkedAccount other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as LinkedAccount);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class LinkedAccount : IComparable, IComparable<LinkedAccount>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as LinkedAccount);
        }

        public int CompareTo([CanBeNull] LinkedAccount other)
        {
            return string.Compare(_value, other?._value, StringComparison.Ordinal);
        }
    }
}