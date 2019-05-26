// Filename: DepositMoneyValidator.cs
// Date Created: 2019-05-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate.Specifications;
using eDoxa.Seedwork.Application.Validators;

namespace eDoxa.Cashier.Domain.Services.Validators
{
    internal class DepositMoneyValidator : DomainValidator<MoneyAccount>
    {
        public DepositMoneyValidator(DateTime? lastDeposit)
        {
            this.AddRule(new DailyMoneyDepositUnavailableSpecification(), $"Deposit unavailable until {lastDeposit?.AddDays(1)}");
        }
    }
}
