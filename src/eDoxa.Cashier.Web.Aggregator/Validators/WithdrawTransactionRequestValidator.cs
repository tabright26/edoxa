// Filename: WithdrawTransactionRequestValidator.cs
// Date Created: 2020-02-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Web.Aggregator.Requests;

using FluentValidation;

namespace eDoxa.Cashier.Web.Aggregator.Validators
{
    public class WithdrawTransactionRequestValidator : AbstractValidator<WithdrawTransactionRequest>
    {
        public WithdrawTransactionRequestValidator()
        {
            this.RuleFor(request => request.Email).NotNull().NotEmpty();
        }
    }
}
