// Filename: AddressPostRequestValidator.cs
// Date Created: 2019-08-23
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Clans.Api.Areas.Clans.ErrorDescribers;
using eDoxa.Clans.Api.Areas.Clans.Requests;
using eDoxa.Seedwork.Application.Validations.Extensions;

using FluentValidation;

namespace eDoxa.Clans.Api.Areas.Clans.Validators
{
    public sealed class CandidaturePostRequestValidator : AbstractValidator<CandidaturePostRequest>
    {
        public CandidaturePostRequestValidator()
        {
            this.EntityId(request => request.UserId).WithMessage(InvitationErrorDescriber.UserIdRequired());

            this.EntityId(request => request.ClanId).WithMessage(CandidatureErrorDescriber.ClanIdRequired());
        }
    }
}