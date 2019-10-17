import Address from "forms/User/Address";

export const MONEY_CURRENCY = "money";
export const TOKEN_CURRENCY = "token";

export type Currency = typeof MONEY_CURRENCY | typeof TOKEN_CURRENCY;

export const TRANSACTION_TYPE_DEPOSIT = "deposit";
export const TRANSACTION_TYPE_REWARD = "reward";
export const TRANSACTION_TYPE_CHARGE = "charge";
export const TRANSACTION_TYPE_PAYOUT = "payout";
export const TRANSACTION_TYPE_WITHDRAWAL = "withdrawal";

export type TransactionType = typeof TRANSACTION_TYPE_DEPOSIT | typeof TRANSACTION_TYPE_REWARD | typeof TRANSACTION_TYPE_CHARGE | typeof TRANSACTION_TYPE_PAYOUT | typeof TRANSACTION_TYPE_WITHDRAWAL;

export const TRANSACTION_STATUS_PENDING = "pending";
export const TRANSACTION_STATUS_SUCCEDED = "succeded";
export const TRANSACTION_STATUS_FAILED = "failed";

export type TransactionStatus = typeof TRANSACTION_STATUS_PENDING | typeof TRANSACTION_STATUS_SUCCEDED | typeof TRANSACTION_STATUS_FAILED;

export type UserId = string;
export type TransactionId = string;
export type AddressId = string;

interface Entity<TEntityId> {
  readonly id: TEntityId;
}

export interface Balance {
  readonly available: number;
  readonly pending: number;
}

export interface Email {
  readonly address: string;
  readonly verified: boolean;
}

export interface Phone {
  readonly number: string;
  readonly verified: boolean;
}

export interface Address extends Entity<AddressId> {
  readonly country: string;
  readonly line1: string;
  readonly line2?: string;
  readonly city: string;
  readonly state?: string;
  readonly postalCode?: string;
}

export interface Doxatag {
  readonly userId: UserId;
  readonly name: string;
  readonly code: number;
  readonly timestamp: number;
}

export interface Transaction extends Entity<TransactionId> {}
