// Filename: WithdrawCommand.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Application.ViewModels;
using eDoxa.Seedwork.Application.Commands.Abstractions;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class WithdrawCommand : Command<TransactionViewModel>
    {
        public WithdrawCommand(decimal amount)
        {
            Amount = amount;
        }

        [DataMember(Name = "amount")]
        public decimal Amount { get; private set; }
    }
}
