﻿// Filename: AddressPostRequestValidator.cs
// Date Created: 2019-08-23
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Text.RegularExpressions;

using eDoxa.Clans.Api.Application.ErrorDescribers;
using eDoxa.Grpc.Protos.Clans.Requests;

using FluentValidation;

namespace eDoxa.Clans.Api.Application.Validators
{
    public sealed class ClanPostRequestValidator : AbstractValidator<CreateClanRequest>
    {
        public ClanPostRequestValidator()
        {
            this.RuleFor(request => request.Name)
                .NotNull()
                .WithMessage(ClanErrorDescriber.NameRequired())
                .NotEmpty()
                .WithMessage(ClanErrorDescriber.NameRequired())
                .Length(3, 20).WithMessage(ClanErrorDescriber.NameLength())
                .Matches(new Regex("^[a-zA-Z0-9- .,]{3,20}$"))
                .WithMessage(ClanErrorDescriber.NameInvalid());
        }
    }
}