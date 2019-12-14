import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Balance, Bundle, Transaction } from "types";

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

export const USER_ACCOUNT_DEPOSIT_MONEY = "USER_ACCOUNT_DEPOSIT_MONEY";
export const USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS =
  "USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS";
export const USER_ACCOUNT_DEPOSIT_MONEY_FAIL =
  "USER_ACCOUNT_DEPOSIT_MONEY_FAIL";

export const USER_ACCOUNT_DEPOSIT_TOKEN = "USER_ACCOUNT_DEPOSIT_TOKEN";
export const USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS =
  "USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS";
export const USER_ACCOUNT_DEPOSIT_TOKEN_FAIL =
  "USER_ACCOUNT_DEPOSIT_TOKEN_FAIL";

export type UserAccountDepositMoneyType =
  | typeof USER_ACCOUNT_DEPOSIT_MONEY
  | typeof USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS
  | typeof USER_ACCOUNT_DEPOSIT_MONEY_FAIL;
export type UserAccountDepositMoneyActionCreator = AxiosActionCreator<
  UserAccountDepositMoneyType
>;
export type UserAccountDepositMoneyAction = AxiosAction<
  UserAccountDepositMoneyType,
  Transaction
>;

export type UserAccountDepositTokenType =
  | typeof USER_ACCOUNT_DEPOSIT_TOKEN
  | typeof USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS
  | typeof USER_ACCOUNT_DEPOSIT_TOKEN_FAIL;
export type UserAccountDepositTokenActionCreator = AxiosActionCreator<
  UserAccountDepositTokenType
>;
export type UserAccountDepositTokenAction = AxiosAction<
  UserAccountDepositTokenType,
  Transaction
>;

export type UserAccountDepositActionCreators =
  | UserAccountDepositMoneyActionCreator
  | UserAccountDepositTokenActionCreator;
export type UserAccountDepositActions =
  | UserAccountDepositMoneyAction
  | UserAccountDepositTokenAction;

export const LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES =
  "LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES";
export const LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS =
  "LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS";
export const LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL =
  "LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL";

export const LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES =
  "LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES";
export const LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS =
  "LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS";
export const LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL =
  "LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL";

export type LoadUserAccountDepositMoneyBundlesType =
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL;
export type LoadUserAccountDepositMoneyBundlesActionCreator = AxiosActionCreator<
  LoadUserAccountDepositMoneyBundlesType
>;
export type LoadUserAccountDepositMoneyBundlesAction = AxiosAction<
  LoadUserAccountDepositMoneyBundlesType,
  Bundle[]
>;

export type LoadUserAccountDepositTokenBundlesType =
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL;
export type LoadUserAccountDepositTokenBundlesActionCreator = AxiosActionCreator<
  LoadUserAccountDepositTokenBundlesType
>;
export type LoadUserAccountDepositTokenBundlesAction = AxiosAction<
  LoadUserAccountDepositTokenBundlesType,
  Bundle[]
>;

export type UserAccountDepositBundlesActionCreators =
  | LoadUserAccountDepositMoneyBundlesActionCreator
  | LoadUserAccountDepositTokenBundlesActionCreator;
export type UserAccountDepositBundlesActions =
  | LoadUserAccountDepositMoneyBundlesAction
  | LoadUserAccountDepositTokenBundlesAction;
export type UserAccountDepositBundlesState = AxiosState<Bundle[]>;

export const LOAD_USER_ACCOUNT_TRANSACTIONS = "LOAD_USER_ACCOUNT_TRANSACTIONS";
export const LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS =
  "LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS";
export const LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL =
  "LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL";

export type LoadUserAccountTransactionsType =
  | typeof LOAD_USER_ACCOUNT_TRANSACTIONS
  | typeof LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS
  | typeof LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL;
export type LoadUserAccountTransactionsActionCreator = AxiosActionCreator<
  LoadUserAccountTransactionsType
>;
export type LoadUserAccountTransactionsAction = AxiosAction<
  LoadUserAccountTransactionsType,
  Transaction[]
>;

export type UserAccountTransactionsActionCreators = LoadUserAccountTransactionsActionCreator;
export type UserAccountTransactionsActions = LoadUserAccountTransactionsAction;
export type UserAccountTransactionsState = AxiosState<Transaction[]>;

export const USER_ACCOUNT_WITHDRAWAL_MONEY = "USER_ACCOUNT_WITHDRAWAL_MONEY";
export const USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS =
  "USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS";
export const USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL =
  "USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL";

export type UserAccountWithdrawalMoneyType =
  | typeof USER_ACCOUNT_WITHDRAWAL_MONEY
  | typeof USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS
  | typeof USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL;
export type UserAccountWithdrawalMoneyActionCreator = AxiosActionCreator<
  UserAccountWithdrawalMoneyType
>;
export type UserAccountWithdrawalMoneyAction = AxiosAction<
  UserAccountWithdrawalMoneyType,
  Transaction
>;

export type UserAccountWithdrawalActionCreators = UserAccountWithdrawalMoneyActionCreator;
export type UserAccountWithdrawalActions = UserAccountWithdrawalMoneyAction;

export const LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES =
  "LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES";
export const LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS =
  "LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS";
export const LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL =
  "LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL";

export type LoadUserAccountWithdrawalMoneyBundlesType =
  | typeof LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES
  | typeof LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS
  | typeof LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL;
export type LoadUserAccountWithdrawalMoneyBundlesActionCreator = AxiosActionCreator<
  LoadUserAccountWithdrawalMoneyBundlesType
>;
export type LoadUserAccountWithdrawalMoneyBundlesAction = AxiosAction<
  LoadUserAccountWithdrawalMoneyBundlesType,
  Bundle[]
>;

export type UserAccountWithdrawalBundlesActionCreators = LoadUserAccountWithdrawalMoneyBundlesActionCreator;
export type UserAccountWithdrawalBundlesActions = LoadUserAccountWithdrawalMoneyBundlesAction;
export type UserAccountWithdrawalBundlesState = AxiosState<Bundle[]>;
