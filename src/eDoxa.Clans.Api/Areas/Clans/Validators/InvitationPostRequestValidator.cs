// Filename: AddressPostRequestValidator.cs
// Date Created: 2019-08-23
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Clans.Api.Areas.Clans.ErrorDescribers;
using eDoxa.Grpc.Protos.Clans.Requests;

using FluentValidation;

namespace eDoxa.Clans.Api.Areas.Clans.Validators
{
    public sealed class InvitationPostRequestValidator : AbstractValidator<SendInvitationRequest>
    {
        public InvitationPostRequestValidator()
        {
            this.RuleFor(request => request.UserId).NotNull().WithMessage(InvitationErrorDescriber.UserIdRequired()).NotEmpty().WithMessage(InvitationErrorDescriber.UserIdRequired());

            this.RuleFor(request => request.ClanId).NotNull().WithMessage(InvitationErrorDescriber.ClanIdRequired()).NotEmpty().WithMessage(InvitationErrorDescriber.ClanIdRequired());
        }
    }
}