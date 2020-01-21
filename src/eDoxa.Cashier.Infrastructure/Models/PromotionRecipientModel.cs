// Filename: PromotionRecipientModel.cs
// Date Created: 2020-01-21
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

#nullable disable

using System;

namespace eDoxa.Cashier.Infrastructure.Models
{
    public class PromotionRecipientModel
    {
        public Guid UserId { get; set; }

        public DateTime RedeemedAt { get; set; }
    }
}
