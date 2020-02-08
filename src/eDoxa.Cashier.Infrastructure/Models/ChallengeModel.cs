// Filename: ChallengePayoutModel.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure.SqlServer;

namespace eDoxa.Cashier.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 3.1.
    /// </remarks>
    public class ChallengeModel : IEntityModel
    {
        public Guid Id { get; set; }

        public int EntryFeeCurrency { get; set; }

        public decimal EntryFeeAmount { get; set; }

        public ICollection<ChallengePayoutBucketModel> PayoutBuckets { get; set; }

        public ICollection<IDomainEvent> DomainEvents { get; set; }
    }
}
