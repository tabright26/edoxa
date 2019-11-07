// Filename: ChallengePostRequestValidator.cs
// Date Created: 2019-11-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Challenges.Requests;

using FluentValidation;

using Microsoft.Extensions.Options;

namespace eDoxa.Challenges.Api.Areas.Challenges.Validators
{
    public class ChallengePostRequestValidator : AbstractValidator<ChallengePostRequest>
    {
        public ChallengePostRequestValidator(IOptions<ChallengeOptions> options)
        {
            this.RuleFor(request => request.Name).Matches(options.Value.Name).WithMessage("The property name is invalid.");
            this.RuleFor(request => request.Game).Must(game => options.Value.Game.Contains(game)).WithMessage("The property game is invalid.");
            this.RuleFor(request => request.BestOf).Must(bestOf => options.Value.BestOf.Contains(bestOf)).WithMessage("The property bestOf is invalid");
            this.RuleFor(request => request.Entries).Must(entries => options.Value.Entries.Contains(entries)).WithMessage("The property entries is invalid.");
            this.RuleFor(request => request.Duration).Must(duration => options.Value.Duration.Contains(duration)).WithMessage("The property duration is invalid.");
        }
    }
}
