// Filename: PayoutModel.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Challenges.Web.Aggregator.Models
{
    public class PayoutModel
    {
        public PrizePoolModel PrizePool { get; set; }

        public BucketModel[] Buckets { get; set; }
    }
}
