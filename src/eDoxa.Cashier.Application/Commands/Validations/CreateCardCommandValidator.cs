using eDoxa.Commands.Abstractions.Validations;

using FluentValidation;

namespace eDoxa.Cashier.Application.Commands.Validations
{
    internal sealed class CreateCardCommandValidator : CommandValidator<CreateBankAccountCommand>
    {
        public CreateCardCommandValidator()
        {
            this.RuleFor(command => command.ExternalAccountTokenId).Must(sourceToken => !string.IsNullOrWhiteSpace(sourceToken)).WithMessage("The source token provided isn't valid.");
        }
    }
}
