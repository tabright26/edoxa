import { TransactionBundle } from "./cashier";
import { GameOptions } from "./games";
import { CountryIsoCode } from "./identity";

export interface LogoutToken {
  readonly logoutId?: string;
  readonly clientName?: string;
  readonly postLogoutRedirectUri?: string;
  readonly signOutIFrameUrl?: string;
  readonly showSignoutPrompt: boolean;
}

export interface ChallengesStaticOptions {}

export interface GamesStaticOptions {
  readonly games: GameOptions[];
}

export interface CashierStaticOptions {
  readonly transaction: TransactionOptions;
  readonly promotion: PromotionOptions;
}

export interface PromotionOptions {}

export interface TransactionOptions {
  readonly bundles: TransactionBundle[];
}

export interface Balance {
  readonly available: number;
  readonly pending: number;
}

export const FIELD_VALIDATION_RULE_TYPE_REQUIRED = "Required";
export const FIELD_VALIDATION_RULE_TYPE_REGEX = "Regex";
export const FIELD_VALIDATION_RULE_TYPE_LENGTH = "Length";
export const FIELD_VALIDATION_RULE_TYPE_MIN_LENGTH = "MinLength";
export const FIELD_VALIDATION_RULE_TYPE_MAX_LENGTH = "MaxLength";

export type FieldValidationRuleType =
  | typeof FIELD_VALIDATION_RULE_TYPE_REQUIRED
  | typeof FIELD_VALIDATION_RULE_TYPE_REGEX
  | typeof FIELD_VALIDATION_RULE_TYPE_LENGTH
  | typeof FIELD_VALIDATION_RULE_TYPE_MIN_LENGTH
  | typeof FIELD_VALIDATION_RULE_TYPE_MAX_LENGTH;

export interface FieldValidationRule {
  readonly type: FieldValidationRuleType;
  readonly message: string;
  readonly value: any;
  readonly enabled: boolean;
  readonly order: number;
}

export interface StripeOptions {
  readonly currencies: CurrenciesOptions;
  readonly paymentMethod: PaymentMethodOptions;
}

export interface CurrenciesOptions {
  readonly ca: string[];
  readonly us: string[];
}

export interface PaymentMethodOptions {
  readonly card: CardOptions;
}

export interface CardOptions {
  readonly limit: number;
}

// Identity
export interface IdentityStaticOptions {
  readonly addressBook: AddressBookOptions;
  readonly countries: CountryOptions[];
}

export interface AddressBookOptions {
  readonly limit: number;
}

export interface CountryOptions {
  readonly isoCode: CountryIsoCode;
  readonly name: string;
  readonly code: string;
  readonly address: AddressOptions;
  readonly regions: CountryRegionOptions[];
}

export interface CountryRegionOptions {
  readonly name: string;
  readonly code: string;
}

export interface AddressOptions {
  readonly fields: AddressFieldsOptions;
  readonly validator: AddressValidatorOptions;
}

export interface AddressFieldsOptions {
  readonly country: { readonly label: string; readonly placeholder: string };
  readonly line1: { readonly label: string; readonly placeholder: string };
  readonly line2: {
    readonly label: string;
    readonly placeholder: string;
    readonly excluded: boolean;
  };
  readonly city: { readonly label: string; readonly placeholder: string };
  readonly state: {
    readonly label: string;
    readonly placeholder: string;
    readonly excluded: boolean;
  };
  readonly postalCode: {
    readonly label: string;
    readonly placeholder: string;
    readonly excluded: boolean;
    readonly mask: string;
  };
}

export interface AddressValidatorOptions {
  readonly line1: FieldValidationRule[];
  readonly line2: FieldValidationRule[];
  readonly city: FieldValidationRule[];
  readonly state: FieldValidationRule[];
  readonly postalCode: FieldValidationRule[];
}
