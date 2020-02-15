export type StripePaymentMethodId = string;
export type StripePaymentMethodCardBrand = stripe.paymentMethod.paymentMethodCardBrand;

export interface StripePaymentMethod {
  readonly id: StripePaymentMethodId;
  readonly card: StripePaymentMethodCard;
}

export interface StripePaymentMethodCard {
  readonly brand: StripePaymentMethodCardBrand;
  readonly country: string;
  readonly expMonth: number;
  readonly expYear: number;
  readonly last4: string;
}
