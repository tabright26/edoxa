// Filename: ForgotPasswordRequestValidator.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Identity.Requests;

using FluentValidation;

namespace eDoxa.Identity.Api.Application.Validators
{
    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidator()
        {
            this.RuleFor(request => request.Email).EmailAddress().WithMessage("Email is invalid");
        }
    }
}
