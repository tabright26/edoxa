// Filename: SetupModel.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel.DataAnnotations.Schema;

namespace eDoxa.Arena.Challenges.Infrastructure.Models
{
    public class ChallengeSetupModel
    {
        public int BestOf { get; set; }

        public int Entries { get; set; }

        public int PayoutEntries { get; set; }
        
        public int EntryFeeCurrency { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal EntryFeeAmount { get; set; }
    }
}
