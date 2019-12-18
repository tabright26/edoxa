// Filename: TransactionMetadata.cs
// Date Created: 2019-12-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class TransactionMetadata : Dictionary<string, string>
    {
        public const string ChallengeKey = "ChallengeId";
        public const string ChallengeParticipantKey = "ChallengeParticipantId";
        public const string UserKey = "UserId";
        
        public TransactionMetadata(IDictionary<string, string> metadata) : base(metadata)
        {
        }

        public TransactionMetadata()
        {
        }
    }
}
