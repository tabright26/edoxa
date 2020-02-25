// Filename: DepositTransactionRequestValidator.cs
// Date Created: 2020-02-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Web.Aggregator.Requests;

using FluentValidation;

namespace eDoxa.Cashier.Web.Aggregator.Validators
{
    public class DepositTransactionRequestValidator : AbstractValidator<DepositTransactionRequest>
    {
        public DepositTransactionRequestValidator()
        {
            this.RuleFor(request => request.PaymentMethodId).NotNull().NotEmpty();
        }
    }
}
