export type StripeCardBrand = "visa" | "amex" | "mastercard" | "discover";

export type StripePaymentMethodId = string;

export interface StripeCustomer {
  readonly defaultPaymentMethodId: StripePaymentMethodId;
}

export interface StripePaymentMethod {
  readonly id: StripePaymentMethodId;
  readonly card: StripeCard;
}

export interface StripeCard {
  readonly brand: StripeCardBrand;
  readonly country: string;
  readonly expMonth: number;
  readonly expYear: number;
  readonly last4: string;
}

export interface StripeAccount {
  readonly enabled: boolean;
}

export interface StripeBankAccount {
  readonly bankName: string;
  readonly country: string;
  readonly currency: string;
  readonly last4: string;
  readonly status: string;
  readonly defaultForCurrency: boolean;
}
