// Filename: SetupModel.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Arena.Challenges.Infrastructure.Models
{
    public class SetupModel
    {
        public int BestOf { get; set; }

        public int Entries { get; set; }

        public decimal EntryFeeAmount { get; set; }

        public int EntryFeeCurrency { get; set; }

        public int PayoutEntries { get; set; }
    }
}
