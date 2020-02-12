// Filename: ChangeDoxatagRequestValidator.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Identity.Requests;

using FluentValidation;

namespace eDoxa.Identity.Api.Application.Validators
{
    public sealed class ChangePhoneRequestValidator : AbstractValidator<ChangePhoneRequest>
    {
        public ChangePhoneRequestValidator()
        {
            this.RuleFor(request => request.Number).NotNull().NotEmpty();
        }
    }
}
