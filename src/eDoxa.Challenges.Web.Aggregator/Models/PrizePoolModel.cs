// Filename: PrizePoolModel.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Cashier.Enums;

namespace eDoxa.Challenges.Web.Aggregator.Models
{
    public class PrizePoolModel
    {
        public Currency Currency { get; set; }

        public double Amount { get; set; }
    }
}
