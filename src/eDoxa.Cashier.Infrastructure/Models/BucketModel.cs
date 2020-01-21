// Filename: BucketModel.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Cashier.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 3.1.
    /// </remarks>
    public class BucketModel
    {
        public int Size { get; set; }

        public int PrizeCurrency { get; set; }

        public decimal PrizeAmount { get; set; }
    }
}
