// Filename: ChallengePostRequestValidator.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Challenges.Api.Application.ErrorDescribers;
using eDoxa.Grpc.Protos.Challenges.Options;
using eDoxa.Grpc.Protos.Challenges.Requests;
using eDoxa.Seedwork.Application.FluentValidation.Extensions;

using FluentValidation;

using Microsoft.Extensions.Options;

namespace eDoxa.Challenges.Api.Application.Validators
{
    public class ChallengePostRequestValidator : AbstractValidator<CreateChallengeRequest>
    {
        public ChallengePostRequestValidator(IOptionsSnapshot<ChallengesApiOptions> optionsSnapshot)
        {
            var challengeOptions = optionsSnapshot.Value.Static.Challenge;

            this.RuleFor(request => request.Name).Custom(challengeOptions.Name.Validators);

            this.RuleFor(request => request.Game)
                .Must(game => challengeOptions.Game.Values.Any(enumGame => enumGame == game))
                .WithMessage(ChallengeErrorDescriber.GameInvalid());

            this.RuleFor(request => request.BestOf)
                .Must(bestOf => challengeOptions.BestOf.Values.Contains(bestOf))
                .WithMessage(ChallengeErrorDescriber.BestOfInvalid());

            this.RuleFor(request => request.Entries)
                .Must(entries => challengeOptions.Entries.Values.Contains(entries))
                .WithMessage(ChallengeErrorDescriber.EntriesInvalid());

            this.RuleFor(request => request.Duration)
                .Must(duration => challengeOptions.Duration.Values.Contains(duration))
                .WithMessage(ChallengeErrorDescriber.DurationInvalid());
        }
    }
}
