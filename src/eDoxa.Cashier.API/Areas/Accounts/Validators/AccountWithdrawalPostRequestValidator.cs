// Filename: AccountWithdrawalPostRequestValidator.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.Api.Areas.Accounts.Requests;
using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Validators;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Areas.Accounts.Validators
{
    public sealed class AccountWithdrawalPostRequestValidator : AbstractValidator<AccountWithdrawalPostRequest>
    {
        public AccountWithdrawalPostRequestValidator(IHttpContextAccessor httpContextAccessor, IAccountQuery accountQuery)
        {
            var amounts = new[] {Money.Fifty, Money.OneHundred, Money.TwoHundred};

            this.RuleFor(request => request.Amount)
                .Must(amount => amounts.Any(money => money.Amount == amount))
                .WithMessage(
                    $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", amounts.Select(amount => amount.Amount))}]."
                )
                .DependentRules(
                    () =>
                    {
                        this.RuleFor(request => request)
                            .CustomAsync(
                                async (request, context, cancellationToken) =>
                                {
                                    var userId = httpContextAccessor.GetUserId();

                                    var account = await accountQuery.FindUserAccountAsync(userId);

                                    if (account == null)
                                    {
                                        context.AddFailure(
                                            new ValidationFailure(string.Empty, "User account not found.")
                                            {
                                                ErrorCode = StatusCodes.Status404NotFound.ToString()
                                            }
                                        );

                                        return;
                                    }

                                    var accountMoney = new MoneyAccount(account);

                                    foreach (var error in new WithdrawalMoneyValidator(new Money(request.Amount)).Validate(accountMoney).Errors)
                                    {
                                        context.AddFailure(error);
                                    }
                                }
                            );
                    }
                );
        }
    }
}
