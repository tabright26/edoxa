// Filename: BucketModel.cs
// Date Created: 2019-06-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Infrastructure;

namespace eDoxa.Arena.Challenges.Infrastructure.Models
{
    public class BucketModel : PersistentObject
    {
        public int Size { get; set; }

        public int PrizeCurrency { get; set; }

        public decimal PrizeAmount { get; set; }
    }
}
