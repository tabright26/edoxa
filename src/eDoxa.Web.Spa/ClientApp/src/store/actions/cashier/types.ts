import { AxiosActionCreator, AxiosAction } from "utils/axios/types";
import { Balance, UserTransaction } from "types";

export type UserAccountBalanceActionCreators =
  | LoadUserMoneyAccountBalanceActionCreator
  | LoadUserTokenAccountBalanceActionCreator;

export const LOAD_USER_MONEY_ACCOUNT_BALANCE =
  "LOAD_USER_MONEY_ACCOUNT_BALANCE";
export const LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS =
  "LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS";
export const LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL =
  "LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL";

export type LoadUserMoneyAccountBalanceType =
  | typeof LOAD_USER_MONEY_ACCOUNT_BALANCE
  | typeof LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS
  | typeof LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL;
export type LoadUserMoneyAccountBalanceActionCreator = AxiosActionCreator<
  LoadUserMoneyAccountBalanceType
>;
export type LoadUserMoneyAccountBalanceAction = AxiosAction<
  LoadUserMoneyAccountBalanceType,
  Balance
>;

export type UserMoneyAccountBalanceActions = LoadUserMoneyAccountBalanceAction;

export const LOAD_USER_TOKEN_ACCOUNT_BALANCE =
  "LOAD_USER_TOKEN_ACCOUNT_BALANCE";
export const LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS =
  "LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS";
export const LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL =
  "LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL";

export type LoadUserTokenAccountBalanceType =
  | typeof LOAD_USER_TOKEN_ACCOUNT_BALANCE
  | typeof LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS
  | typeof LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL;
export type LoadUserTokenAccountBalanceActionCreator = AxiosActionCreator<
  LoadUserTokenAccountBalanceType
>;
export type LoadUserTokenAccountBalanceAction = AxiosAction<
  LoadUserTokenAccountBalanceType,
  Balance
>;

export type UserTokenAccountBalanceActions = LoadUserTokenAccountBalanceAction;

export const CREATE_USER_TRANSACTION = "CREATE_USER_TRANSACTION";
export const CREATE_USER_TRANSACTION_SUCCESS =
  "CREATE_USER_TRANSACTION_SUCCESS";
export const CREATE_USER_TRANSACTION_FAIL = "CREATE_USER_TRANSACTION_FAIL";

export type CreateUserTransactionType =
  | typeof CREATE_USER_TRANSACTION
  | typeof CREATE_USER_TRANSACTION_SUCCESS
  | typeof CREATE_USER_TRANSACTION_FAIL;
export type CreateUserTransactionActionCreator = AxiosActionCreator<
  CreateUserTransactionType
>;
export type CreateUserTransactionAction = AxiosAction<
  CreateUserTransactionType,
  UserTransaction
>;

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
  UserTransaction[]
>;

export const REDEEM_PROMOTIONAL_CODE = "REDEEM_PROMOTIONAL_CODE";
export const REDEEM_PROMOTIONAL_CODE_SUCCESS =
  "REDEEM_PROMOTIONAL_CODE_SUCCESS";
export const REDEEM_PROMOTIONAL_CODE_FAIL = "REDEEM_PROMOTIONAL_CODE_FAIL";

export type RedeemPromotionalCodeType =
  | typeof REDEEM_PROMOTIONAL_CODE
  | typeof REDEEM_PROMOTIONAL_CODE_SUCCESS
  | typeof REDEEM_PROMOTIONAL_CODE_FAIL;
export type RedeemPromotionalCodeActionCreator = AxiosActionCreator<
  RedeemPromotionalCodeType
>;
export type RedeemPromotionalCodeAction = AxiosAction<
  RedeemPromotionalCodeType,
  UserTransaction
>;

export type UserTransactionActionCreators =
  | LoadUserTransactionHistoryActionCreator
  | CreateUserTransactionActionCreator
  | RedeemPromotionalCodeActionCreator;
export type UserTransactionActions =
  | LoadUserTransactionHistoryAction
  | CreateUserTransactionAction
  | RedeemPromotionalCodeAction;
