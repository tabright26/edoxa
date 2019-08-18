// Filename: ChallengeModel.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;

namespace eDoxa.Cashier.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 2.2.
    /// </remarks>
    public class ChallengeModel
    {
        public Guid Id { get; set; }

        public int EntryFeeCurrency { get; set; }

        public decimal EntryFeeAmount { get; set; }

        public ICollection<BucketModel> Buckets { get; set; }
    }
}
