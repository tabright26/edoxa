// Filename: WithdrawalCommand.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Commands.Abstractions;

namespace eDoxa.Cashier.Api.Application.Commands
{
    [DataContract]
    public sealed class WithdrawalCommand : Command
    {
        public WithdrawalCommand(decimal amount)
        {
            Amount = amount;
        }

        [DataMember(Name = "amount")]
        public decimal Amount { get; private set; }
    }
}
