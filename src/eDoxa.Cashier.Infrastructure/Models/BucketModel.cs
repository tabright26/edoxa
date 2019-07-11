// Filename: BucketModel.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Infrastructure;

namespace eDoxa.Cashier.Infrastructure.Models
{
    public class BucketModel : PersistentObject
    {
        public int Size { get; set; }

        public int PrizeCurrency { get; set; }

        public decimal PrizeAmount { get; set; }
    }
}
