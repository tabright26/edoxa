// Filename: ChallengePostRequestValidator.cs
// Date Created: 2019-11-05
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Challenges.Api.Areas.Challenges.ErrorDescribers;
using eDoxa.Challenges.Requests;

using FluentValidation;

using Microsoft.Extensions.Options;

namespace eDoxa.Challenges.Api.Areas.Challenges.Validators
{
    public class ChallengePostRequestValidator : AbstractValidator<CreateChallengeRequest>
    {
        public ChallengePostRequestValidator(IOptions<ChallengeOptions> options)
        {
            this.RuleFor(request => request.Name).Matches(options.Value.Name).WithMessage(ChallengeErrorDescriber.NameInvalid());
            this.RuleFor(request => request.Game).Must(game => options.Value.Game.Contains(game)).WithMessage(ChallengeErrorDescriber.GameInvalid());
            this.RuleFor(request => request.BestOf).Must(bestOf => options.Value.BestOf.Contains(bestOf)).WithMessage(ChallengeErrorDescriber.BestOfInvalid());
            this.RuleFor(request => request.Entries).Must(entries => options.Value.Entries.Contains(entries)).WithMessage(ChallengeErrorDescriber.EntriesInvalid());
            this.RuleFor(request => request.Duration).Must(duration => options.Value.Duration.Contains(duration)).WithMessage(ChallengeErrorDescriber.DurationInvalid());
        }
    }
}
