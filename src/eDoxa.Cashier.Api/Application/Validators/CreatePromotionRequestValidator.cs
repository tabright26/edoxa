// Filename: CreatePromotionRequestValidator.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Cashier.Options;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Seedwork.Application.FluentValidation.Extensions;

using FluentValidation;

using Microsoft.Extensions.Options;

namespace eDoxa.Cashier.Api.Application.Validators
{
    public sealed class CreatePromotionRequestValidator : AbstractValidator<CreatePromotionRequest>
    {
        public CreatePromotionRequestValidator(IOptionsSnapshot<CashierApiOptions> snapshot)
        {
            var promotionOptions = snapshot.Value.Static.Promotion;

            this.RuleFor(request => request.PromotionalCode).Custom(promotionOptions.PromotionalCode.Validators);

            //this.RuleFor(request => request.Currency).Custom(promotionOptions.Currency.Validators);

            //this.RuleFor(request => request.Duration).Custom(promotionOptions.Duration.Validators);
        }
    }
}
