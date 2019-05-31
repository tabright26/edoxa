using FluentValidation;

using Stripe;

namespace eDoxa.Stripe.Validators
{
    public class StripeCustomerValidator : AbstractValidator<Customer>
    {
        public StripeCustomerValidator()
        {
            this.RuleFor(customer => customer.DefaultSource).NotNull().WithMessage("There are no credit cards associated with this account.")
                .DependentRules(
                    () =>
                    {
                        this.RuleFor(customer => customer.DefaultSource.Object).Must(defaultSourceType => defaultSourceType == "card").WithMessage("The default source card is not a credit card.");
                    });
        }
    }
}
