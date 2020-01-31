// Filename: MatchModel.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure.SqlServer;

namespace eDoxa.Challenges.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 3.1.
    /// </remarks>
    public class MatchModel : IEntityModel
    {
        public Guid Id { get; set; }

        public string GameUuid { get; set; }

        public DateTime GameStartedAt { get; set; }

        public long GameDuration { get; set; }

        public DateTime SynchronizedAt { get; set; }

        public ParticipantModel Participant { get; set; }

        public ICollection<StatModel> Stats { get; set; }

        public ICollection<IDomainEvent> DomainEvents { get; set; }
    }
}
