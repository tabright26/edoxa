// Filename: WithdrawMoneyValidator.cs
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
    public class WithdrawMoneyValidator : DomainValidator<MoneyAccount>
    {
        public WithdrawMoneyValidator(Money money, DateTime? lastWithdrawal)
        {
            this.AddRule(new HasBankAccountSpecification().Not(), "A bank account is required to withdrawal.");

            this.AddRule(new InsufficientMoneySpecification(money).Not(), "Insufficient funds.");

            this.AddRule(new WeeklyMoneyWithdrawUnavailableSpecification(), $"Withdraw unavailable until {lastWithdrawal?.AddDays(7)}");
        }
    }
}
