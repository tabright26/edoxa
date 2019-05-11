// Filename: WithdrawalFundsCommand.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Commands.Abstractions;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class WithdrawalFundsCommand : Command<IActionResult>
    {
        public WithdrawalFundsCommand(WithdrawalMoneyBundleType bundleType)
        {
            BundleType = bundleType;
        }

        [DataMember(Name = "bundleType")] public WithdrawalMoneyBundleType BundleType { get; private set; }
    }
}