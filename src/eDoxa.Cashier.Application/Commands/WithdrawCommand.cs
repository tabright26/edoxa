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

using eDoxa.Cashier.DTO;
using eDoxa.Commands.Abstractions;
using eDoxa.Functional;

using FluentValidation.Results;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class WithdrawCommand : Command<Either<ValidationResult, TransactionDTO>>
    {
        public WithdrawCommand(decimal amount)
        {
            Amount = amount;
        }

        [DataMember(Name = "amount")]
        public decimal Amount { get; private set; }
    }
}
