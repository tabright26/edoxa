import { AxiosActionCreator, AxiosAction } from "utils/axios/types";
import { Transaction, Promotion } from "types";

export const DEPOSIT_TRANSACTION = "DEPOSIT_TRANSACTION";
export const DEPOSIT_TRANSACTION_SUCCESS = "DEPOSIT_TRANSACTION_SUCCESS";
export const DEPOSIT_TRANSACTION_FAIL = "DEPOSIT_TRANSACTION_FAIL";

export type DepositTransactionType =
  | typeof DEPOSIT_TRANSACTION
  | typeof DEPOSIT_TRANSACTION_SUCCESS
  | typeof DEPOSIT_TRANSACTION_FAIL;
export type DepositTransactionActionCreator = AxiosActionCreator<
  DepositTransactionType
>;
export type DepositTransactionAction = AxiosAction<DepositTransactionType>;

export const WITHDRAW_TRANSACTION = "WITHDRAW_TRANSACTION";
export const WITHDRAW_TRANSACTION_SUCCESS = "WITHDRAW_TRANSACTION_SUCCESS";
export const WITHDRAW_TRANSACTION_FAIL = "WITHDRAW_TRANSACTION_FAIL";

export type WithdrawTransactionType =
  | typeof WITHDRAW_TRANSACTION
  | typeof WITHDRAW_TRANSACTION_SUCCESS
  | typeof WITHDRAW_TRANSACTION_FAIL;
export type WithdrawTransactionActionCreator = AxiosActionCreator<
  WithdrawTransactionType
>;
export type WithdrawTransactionAction = AxiosAction<WithdrawTransactionType>;

export const LOAD_USER_TRANSACTION_HISTORY = "LOAD_USER_TRANSACTION_HISTORY";
export const LOAD_USER_TRANSACTION_HISTORY_SUCCESS =
  "LOAD_USER_TRANSACTION_HISTORY_SUCCESS";
export const LOAD_USER_TRANSACTION_HISTORY_FAIL =
  "LOAD_USER_TRANSACTION_HISTORY_FAIL";

export type LoadUserTransactionHistoryType =
  | typeof LOAD_USER_TRANSACTION_HISTORY
  | typeof LOAD_USER_TRANSACTION_HISTORY_SUCCESS
  | typeof LOAD_USER_TRANSACTION_HISTORY_FAIL;
export type LoadUserTransactionHistoryActionCreator = AxiosActionCreator<
  LoadUserTransactionHistoryType
>;
export type LoadUserTransactionHistoryAction = AxiosAction<
  LoadUserTransactionHistoryType,
  Transaction[]
>;

export const REDEEM_PROMOTION = "REDEEM_PROMOTION";
export const REDEEM_PROMOTION_SUCCESS = "REDEEM_PROMOTION_SUCCESS";
export const REDEEM_PROMOTION_FAIL = "REDEEM_PROMOTION_FAIL";

export type RedeemPromotionType =
  | typeof REDEEM_PROMOTION
  | typeof REDEEM_PROMOTION_SUCCESS
  | typeof REDEEM_PROMOTION_FAIL;
export type RedeemPromotionActionCreator = AxiosActionCreator<
  RedeemPromotionType
>;
export type RedeemPromotionAction = AxiosAction<RedeemPromotionType, Promotion>;
