// Filename: ChallengeSetupModel.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Arena.Challenges.Infrastructure.Models
{
    public class ChallengeSetupModel
    {
        public int BestOf { get; set; }

        public int Entries { get; set; }

        public int PayoutEntries { get; set; }

        public int EntryFeeCurrency { get; set; }

        public decimal EntryFeeAmount { get; set; }
    }
}
