export const MONEY_CURRENCY = "money";
export const TOKEN_CURRENCY = "token";

export type Currency = typeof MONEY_CURRENCY | typeof TOKEN_CURRENCY;

export interface Balance {
  available: number;
  pending: number;
}

export interface Email {
  address: string;
  verified: boolean;
}

export interface Phone {
  number: string;
  verified: boolean;
}

export interface Address {
  id: string;
  country: string;
  line1: string;
  line2?: string;
  city: string;
  state?: string;
  postalCode?: string;
}

export interface Doxatag {
  userId: string;
  name: string;
  code: number;
  timestamp: number;
}
