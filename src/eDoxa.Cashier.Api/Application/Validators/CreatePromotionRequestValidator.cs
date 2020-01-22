// Filename: CreatePromotionRequestValidator.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Cashier.Requests;

using FluentValidation;

namespace eDoxa.Cashier.Api.Application.Validators
{
    public sealed class CreatePromotionRequestValidator : AbstractValidator<CreatePromotionRequest>
    {
    }
}
