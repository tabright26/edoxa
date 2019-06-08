﻿// Filename: ExternalAccount.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate
{
    public sealed class ExternalAccount : ValueObject
    {
        public ExternalAccount(string externalAccount)
        {
            if (string.IsNullOrWhiteSpace(externalAccount) || !externalAccount.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'))
            {
                throw new ArgumentException(nameof(externalAccount));
            }

            Value = externalAccount;
        }

        public ExternalAccount(Guid externalAccount)
        {
            if (externalAccount == Guid.Empty)
            {
                throw new ArgumentException(nameof(externalAccount));
            }

            Value = externalAccount.ToString();
        }

        public string Value { get; private set; }

        public static implicit operator ExternalAccount(string externalAccount)
        {
            return new ExternalAccount(externalAccount);
        }

        public static implicit operator ExternalAccount(Guid externalAccount)
        {
            return new ExternalAccount(externalAccount);
        }

        public override string ToString()
        {
            return Value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
