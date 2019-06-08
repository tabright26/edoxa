// Filename: DeleteCardCommand.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Seedwork.Application.Commands.Abstractions;
using eDoxa.Stripe.Models;

namespace eDoxa.Cashier.Api.Application.Commands
{
    [DataContract]
    public sealed class DeleteCardCommand : Command
    {
        public DeleteCardCommand(StripeCardId cardId)
        {
            StripeCardId = cardId;
        }

        [IgnoreDataMember] public StripeCardId StripeCardId { get; private set; }
    }
}
