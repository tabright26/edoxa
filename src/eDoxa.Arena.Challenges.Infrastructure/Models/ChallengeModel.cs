// Filename: ChallengeModel.cs
// Date Created: 2019-06-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Infrastructure;

namespace eDoxa.Arena.Challenges.Infrastructure.Models
{
    public class ChallengeModel : PersistentObject
    {
        public string Name { get; set; }

        public int Game { get; set; }

        public int State { get; set; }

        public int BestOf { get; set; }

        public int Entries { get; set; }

        public DateTime? SynchronizedAt { get; set; }

        public ChallengeTimelineModel Timeline { get; set; }

        public ICollection<ScoringItemModel> ScoringItems { get; set; }

        public ICollection<ParticipantModel> Participants { get; set; }
    }
}
