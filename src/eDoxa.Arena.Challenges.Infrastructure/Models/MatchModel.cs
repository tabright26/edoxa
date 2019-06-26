// Filename: MatchModel.cs
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
    public class MatchModel : PersistentObject
    {
        public DateTime SynchronizedAt { get; set; }

        public string GameReference { get; set; }

        public ParticipantModel Participant { get; set; }

        public ICollection<StatModel> Stats { get; set; }
    }
}
