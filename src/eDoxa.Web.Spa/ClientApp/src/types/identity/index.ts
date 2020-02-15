export const COUNTRY_CA = "CA";
export const COUNTRY_US = "US";

export const GENDER_MALE = "Male";
export const GENDER_FEMALE = "Female";
export const GENDER_OTHER = "Other";

export type Country = typeof COUNTRY_CA | typeof COUNTRY_US;
export type Gender =
  | typeof GENDER_MALE
  | typeof GENDER_FEMALE
  | typeof GENDER_OTHER;

export type UserId = string;
export type AddressId = string;
export type FirstName = string;
export type LastName = string;

export interface UserProfile {
  readonly firstName: FirstName;
  readonly lastName: LastName;
  readonly gender: Gender;
}

export interface UserDob {
  readonly year: number;
  readonly month: number;
  readonly day: number;
}

export interface Email {
  readonly address: string;
  readonly verified: boolean;
}

export interface Phone {
  readonly number: string;
  readonly verified: boolean;
}

export interface Address {
  readonly id: AddressId;
  readonly countryIsoCode: Country;
  readonly line1: string;
  readonly line2?: string;
  readonly city: string;
  readonly state?: string;
  readonly postalCode?: string;
}

export interface Doxatag {
  readonly userId: string;
  readonly name: string;
  readonly code: number;
  readonly timestamp: number;
}

export interface AccountLogoutToken {
  readonly logoutId?: string;
  readonly clientName?: string;
  readonly postLogoutRedirectUri?: string;
  readonly signOutIFrameUrl?: string;
  readonly showSignoutPrompt: boolean;
}
