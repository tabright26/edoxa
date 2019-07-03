// Filename: DepositCommand.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Domain.ViewModels;
using eDoxa.Commands.Abstractions;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Cashier.Api.Application.Commands
{
    [DataContract]
    public sealed class DepositCommand : Command
    {
        public DepositCommand(decimal amount, CurrencyType type)
        {
            Currency = new CurrencyViewModel
            {
                Amount = amount,
                Type = type
            };
        }

        [DataMember(Name = "currency")]
        public CurrencyViewModel Currency { get; private set; }
    }
}
