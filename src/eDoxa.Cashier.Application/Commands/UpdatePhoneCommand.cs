// Filename: UpdatePhoneCommand.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Commands.Abstractions;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class UpdatePhoneCommand : Command
    {
        public UpdatePhoneCommand(CustomerId customerId, string phone)
        {
            CustomerId = customerId;
            Phone = phone;
        }

        [DataMember(Name = "customerId")] public CustomerId CustomerId { get; private set; }

        [DataMember(Name = "phone")] public string Phone { get; private set; }
    }
}