// Filename: ChallengePostRequestValidator.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Challenges.Api.Application.ErrorDescribers;
using eDoxa.Grpc.Protos.Challenges.Requests;

using FluentValidation;

using Microsoft.Extensions.Options;

namespace eDoxa.Challenges.Api.Application.Validators
{
    public class ChallengePostRequestValidator : AbstractValidator<CreateChallengeRequest>
    {
        public ChallengePostRequestValidator(IOptions<ChallengeOptions> options)
        {
            this.RuleFor(request => request.Name)
                .NotNull()
                .WithMessage(ChallengeErrorDescriber.NameInvalid())
                .NotEmpty()
                .WithMessage(ChallengeErrorDescriber.NameInvalid())
                .Matches(options.Value.Name)
                .WithMessage(ChallengeErrorDescriber.NameInvalid());

            this.RuleFor(request => request.Game).Must(game => options.Value.Game.Any(gameName => string.Equals(gameName, game.ToString(), StringComparison.InvariantCultureIgnoreCase))).WithMessage(ChallengeErrorDescriber.GameInvalid());
            this.RuleFor(request => request.BestOf).Must(bestOf => options.Value.BestOf.Contains(bestOf)).WithMessage(ChallengeErrorDescriber.BestOfInvalid());

            this.RuleFor(request => request.Entries)
                .Must(entries => options.Value.Entries.Contains(entries))
                .WithMessage(ChallengeErrorDescriber.EntriesInvalid());

            this.RuleFor(request => request.Duration)
                .Must(duration => options.Value.Duration.Contains(duration))
                .WithMessage(ChallengeErrorDescriber.DurationInvalid());
        }
    }
}
