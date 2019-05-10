// Filename: WithdrawalMoneyCommand.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Commands.Abstractions;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class WithdrawalMoneyCommand : Command<IActionResult>
    {
        public WithdrawalMoneyCommand(decimal amount)
        {
            Amount = amount;
        }

        [DataMember(Name = "amount")] public decimal Amount { get; private set; }
    }
}