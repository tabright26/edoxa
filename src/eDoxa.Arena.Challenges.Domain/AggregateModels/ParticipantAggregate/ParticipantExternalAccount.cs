// Filename: ParticipantExternalAccount.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate
{
    public sealed class ParticipantExternalAccount : TypeObject<ParticipantExternalAccount, string>
    {
        public ParticipantExternalAccount(string externalAccount) : base(externalAccount)
        {
            if (string.IsNullOrWhiteSpace(externalAccount) || !externalAccount.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'))
            {
                throw new ArgumentException(nameof(externalAccount));
            }
        }

        public ParticipantExternalAccount(Guid externalAccount) : base(externalAccount.ToString())
        {
            if (externalAccount == Guid.Empty)
            {
                throw new ArgumentException(nameof(externalAccount));
            }
        }

        public static implicit operator ParticipantExternalAccount(string externalAccount)
        {
            return new ParticipantExternalAccount(externalAccount);
        }

        public static implicit operator ParticipantExternalAccount(Guid externalAccount)
        {
            return new ParticipantExternalAccount(externalAccount);
        }
    }
}
