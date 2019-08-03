// Filename: ChallengeModel.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;

using eDoxa.Seedwork.Infrastructure;

namespace eDoxa.Cashier.Infrastructure.Models
{
    public class ChallengeModel : PersistentObject
    {
        public int EntryFeeCurrency { get; set; }

        public decimal EntryFeeAmount { get; set; }

        public ICollection<BucketModel> Buckets { get; set; }
    }
}
