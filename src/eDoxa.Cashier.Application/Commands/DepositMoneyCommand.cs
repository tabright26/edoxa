// Filename: DepositMoneyCommand.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Commands.Abstractions;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class DepositMoneyCommand : Command<IActionResult>
    {
        public DepositMoneyCommand(MoneyBundleType bundleType)
        {
            BundleType = bundleType;
        }
        
        [DataMember(Name = "bundleType")] public MoneyBundleType BundleType { get; private set; }
    }
}