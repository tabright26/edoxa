// Filename: UserDoxatag.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate
{
    public sealed class Doxatag : Entity<DoxatagId>, IAggregateRoot
    {
        public Doxatag(UserId userId, string name, int code, IDateTimeProvider provider)
        {
            UserId = userId;
            Name = name;
            Code = code;
            Timestamp = provider.DateTime;
        }

        public UserId UserId { get; private set; }

        public string Name { get; private set; }

        public int Code { get; private set; }

        public DateTime Timestamp { get; private set; }

        public override string ToString()
        {
            return $"{Name}#{Code}";
        }
    }
}
