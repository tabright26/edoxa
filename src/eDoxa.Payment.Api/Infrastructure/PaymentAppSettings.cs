// Filename: PaymentAppSettings.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.Options;

namespace eDoxa.Payment.Api.Infrastructure
{
    public sealed class PaymentAppSettings
    {
        [Required]
        public AuthorityOptions Authority { get; set; }
    }
}
