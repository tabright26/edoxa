// Filename: ChallengePayoutBucketModel.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

namespace eDoxa.Cashier.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 3.1.
    /// </remarks>
    public class ChallengePayoutBucketModel
    {
        public Guid Id { get; set; }

        public Guid ChallengeId { get; set; }

        public int Size { get; set; }

        public int PrizeCurrency { get; set; }

        public decimal PrizeAmount { get; set; }
    }
}
