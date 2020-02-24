// Filename: ResetPasswordRequestValidator.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Identity.Requests;

using FluentValidation;

namespace eDoxa.Identity.Api.Application.Validators
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            this.RuleFor(request => request.UserId).NotNull().NotEmpty();
            this.RuleFor(request => request.Password).NotNull().NotEmpty();
        }
    }
}
