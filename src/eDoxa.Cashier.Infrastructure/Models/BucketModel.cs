// Filename: BucketModel.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Cashier.Infrastructure.Models
{
    public class BucketModel
    {
        public int Size { get; set; }

        public int PrizeCurrency { get; set; }

        public decimal PrizeAmount { get; set; }
    }
}
