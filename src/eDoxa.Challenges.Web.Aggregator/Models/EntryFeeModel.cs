// Filename: EntryFeeModel.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Cashier.Enums;

namespace eDoxa.Challenges.Web.Aggregator.Models
{
    public class EntryFeeModel
    {
        public Currency Currency { get; set; }

        public double Amount { get; set; }
    }
}
