// Filename: MatchModel.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure;

namespace eDoxa.Challenges.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 2.2.
    /// </remarks>
    public class MatchModel : IEntityModel
    {
        public Guid Id { get; set; }

        public string GameUuid { get; set; }

        public ParticipantModel Participant { get; set; }

        public ICollection<StatModel> Stats { get; set; }

        public ICollection<IDomainEvent> DomainEvents { get; set; }
    }
}
