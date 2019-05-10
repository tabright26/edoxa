// Filename: UpdateEmailCommand.cs
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

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class UpdateEmailCommand : Command
    {
        public UpdateEmailCommand(CustomerId customerId, string email)
        {
            CustomerId = customerId;
            Email = email;
        }

        [DataMember(Name = "customerId")] public CustomerId CustomerId { get; private set; }

        [DataMember(Name = "email")] public string Email { get; private set; }
    }
}