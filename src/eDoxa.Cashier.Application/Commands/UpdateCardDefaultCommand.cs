// Filename: UpdateCardDefaultCommand.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Commands.Abstractions;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class UpdateCardDefaultCommand : Command<IActionResult>
    {
        public UpdateCardDefaultCommand(StripeCardId cardId)
        {
            StripeCardId = cardId;
        }

        [IgnoreDataMember] public StripeCardId StripeCardId { get; private set; }
    }
}