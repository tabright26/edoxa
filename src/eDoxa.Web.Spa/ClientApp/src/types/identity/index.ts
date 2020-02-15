export const COUNTRY_ISO_CODE_CA = "CA";
export const COUNTRY_ISO_CODE_US = "US";

export const GENDER_MALE = "Male";
export const GENDER_FEMALE = "Female";
export const GENDER_OTHER = "Other";

export type CountryIsoCode =
  | typeof COUNTRY_ISO_CODE_CA
  | typeof COUNTRY_ISO_CODE_US;

export type Gender =
  | typeof GENDER_MALE
  | typeof GENDER_FEMALE
  | typeof GENDER_OTHER;

export type UserId = string;
export type AddressId = string;

export interface UserEmail {
  readonly address: string;
  readonly verified: boolean;
}

export interface UserPhone {
  readonly number: string;
  readonly verified: boolean;
}

export interface UserProfile {
  readonly firstName: string;
  readonly lastName: string;
  readonly gender: Gender;
}

export interface UserDob {
  readonly year: number;
  readonly month: number;
  readonly day: number;
}

export interface UserAddress {
  readonly id: AddressId;
  readonly countryIsoCode: CountryIsoCode;
  readonly line1: string;
  readonly line2?: string;
  readonly city: string;
  readonly state?: string;
  readonly postalCode?: string;
}

export interface UserDoxatag {
  readonly userId: string;
  readonly name: string;
  readonly code: number;
  readonly timestamp: number;
}
