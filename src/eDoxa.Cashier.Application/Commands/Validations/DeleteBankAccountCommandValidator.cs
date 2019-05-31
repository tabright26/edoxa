// Filename: DeleteBankAccountCommandValidator.cs
// Date Created: 2019-05-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Validators;
using eDoxa.Commands.Abstractions.Validations;
using eDoxa.Functional.Extensions;
using eDoxa.Security.Extensions;

using FluentValidation;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Application.Commands.Validations
{
    public class DeleteBankAccountCommandValidator : CommandValidator<DeleteBankAccountCommand>
    {
        public DeleteBankAccountCommandValidator(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            this.RuleFor(command => command)
                .CustomAsync(
                    async (command, context, cancellationToken) =>
                    {
                        var userId = httpContextAccessor.GetUserId();

                        var user = await userRepository.GetUserAsNoTrackingAsync(userId);

                        new RemoveBankAccountValidator().Validate(user).Errors.ForEach(context.AddFailure);
                    }
                );
        }
    }
}
