export const CURRENCY_TYPE_MONEY = "money";
export const CURRENCY_TYPE_TOKEN = "token";
export const CURRENCY_TYPE_ALL = "all";

export const TRANSACTION_TYPE_DEPOSIT = "Deposit";
export const TRANSACTION_TYPE_REWARD = "Reward";
export const TRANSACTION_TYPE_CHARGE = "Charge";
export const TRANSACTION_TYPE_PAYOUT = "Payout";
export const TRANSACTION_TYPE_WITHDRAW = "Withdraw";
export const TRANSACTION_TYPE_PROMOTION = "Promotion";
export const TRANSACTION_TYPE_ALL = "All";

export const TRANSACTION_STATUS_PENDING = "Pending";
export const TRANSACTION_STATUS_SUCCEEDED = "Succeeded";
export const TRANSACTION_STATUS_FAILED = "Failed";

export type CurrencyType =
  | typeof CURRENCY_TYPE_MONEY
  | typeof CURRENCY_TYPE_TOKEN
  | typeof CURRENCY_TYPE_ALL;

export type TransactionStatus =
  | typeof TRANSACTION_STATUS_PENDING
  | typeof TRANSACTION_STATUS_SUCCEEDED
  | typeof TRANSACTION_STATUS_FAILED;

export type TransactionType =
  | typeof TRANSACTION_TYPE_DEPOSIT
  | typeof TRANSACTION_TYPE_REWARD
  | typeof TRANSACTION_TYPE_CHARGE
  | typeof TRANSACTION_TYPE_PAYOUT
  | typeof TRANSACTION_TYPE_WITHDRAW
  | typeof TRANSACTION_TYPE_PROMOTION
  | typeof TRANSACTION_TYPE_ALL;

export type TransactionId = string;
export type TransactionBundleId = number;

export type Currency = {
  readonly amount: number;
  readonly type: CurrencyType;
};

export type EntryFee = Currency;

export type PrizePool = Currency;

export interface Transaction {
  readonly id: TransactionId;
  readonly timestamp: number;
  readonly currency: Currency;
  readonly description: string;
  readonly type: TransactionType;
  readonly status: TransactionStatus;
}

export interface TransactionBundle {
  readonly id: TransactionBundleId;
  readonly currency: Currency;
  readonly price: Currency;
  readonly type: TransactionType;
  readonly description: string;
  readonly notes: string;
  readonly disabled: boolean;
  readonly deprecated: boolean;
}

export interface Promotion {
  readonly promotionalCode: string;
  readonly currency: Currency;
}
