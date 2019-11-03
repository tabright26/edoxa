// Filename: MatchModel.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;

namespace eDoxa.Challenges.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 2.2.
    /// </remarks>
    public class MatchModel
    {
        public Guid Id { get; set; }

        public DateTime SynchronizedAt { get; set; }

        public string GameReference { get; set; }

        public ParticipantModel Participant { get; set; }

        public ICollection<StatModel> Stats { get; set; }
    }
}
