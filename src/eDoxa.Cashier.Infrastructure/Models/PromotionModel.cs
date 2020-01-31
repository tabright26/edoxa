// Filename: PromotionModel.cs
// Date Created: 2020-01-20
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure.SqlServer;

namespace eDoxa.Cashier.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 3.1.
    /// </remarks>
    public class PromotionModel : IEntityModel
    {
        public Guid Id { get; set; }

        public string PromotionalCode { get; set; }

        public decimal Amount { get; set; }

        public int Currency { get; set; }

        public long Duration { get; set; }

        public DateTime ExpiredAt { get; set; }

        public DateTime? CanceledAt { get; set; }

        public ICollection<PromotionRecipientModel> Recipients { get; set; }

        public ICollection<IDomainEvent> DomainEvents { get; set; }
    }
}
