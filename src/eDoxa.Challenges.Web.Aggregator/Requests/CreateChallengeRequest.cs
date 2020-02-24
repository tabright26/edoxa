// Filename: CreateChallengeRequest.cs
// Date Created: 2020-02-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

#nullable disable

using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Challenges.Enums;
using eDoxa.Grpc.Protos.Games.Enums;

namespace eDoxa.Challenges.Web.Aggregator.Requests
{
    public class CreateChallengeRequest
    {
        public string Name { get; set; }

        public EnumGame Game { get; set; }

        public EnumChallengeType Type { get; set; }

        public int BestOf { get; set; }

        public int Entries { get; set; }

        public int Duration { get; set; }

        public Currency EntryFee { get; set; }

        public class Currency
        {
            public decimal Amount { get; set; }

            public EnumCurrencyType Type { get; set; }
        }
    }
}
