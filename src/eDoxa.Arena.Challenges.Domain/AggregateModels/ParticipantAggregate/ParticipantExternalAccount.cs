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
    public sealed partial class ParticipantExternalAccount
    {
        private readonly string _value;

        public ParticipantExternalAccount(string externalAccount)
        {
            if (string.IsNullOrWhiteSpace(externalAccount) ||
                !externalAccount.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'))
            {
                throw new ArgumentException(nameof(externalAccount));
            }

            _value = externalAccount;
        }

        public ParticipantExternalAccount(Guid externalAccount)
        {
            if (externalAccount == Guid.Empty)
            {
                throw new ArgumentException(nameof(externalAccount));
            }

            _value = externalAccount.ToString();
        }

        public static implicit operator ParticipantExternalAccount(string externalAccount)
        {
            return new ParticipantExternalAccount(externalAccount);
        }

        public static implicit operator ParticipantExternalAccount(Guid externalAccount)
        {
            return new ParticipantExternalAccount(externalAccount);
        }

        public override string ToString()
        {
            return _value;
        }
    }

    public partial class ParticipantExternalAccount : IEquatable<ParticipantExternalAccount>
    {
        public bool Equals([CanBeNull] ParticipantExternalAccount other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as ParticipantExternalAccount);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class ParticipantExternalAccount : IComparable, IComparable<ParticipantExternalAccount>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as ParticipantExternalAccount);
        }

        public int CompareTo([CanBeNull] ParticipantExternalAccount other)
        {
            return string.Compare(_value, other?._value, StringComparison.Ordinal);
        }
    }
}