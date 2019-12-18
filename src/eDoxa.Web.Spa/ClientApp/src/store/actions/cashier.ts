import {
  AxiosActionCreator,
  AxiosAction,
  AxiosState,
  AxiosPayload
} from "utils/axios/types";
import {
  Bundle,
  Currency,
  MONEY,
  TOKEN,
  TransactionType,
  TRANSACTION_TYPE_DEPOSIT,
  TRANSACTION_TYPE_WITHDRAWAL
} from "types";

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

export const LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES =
  "LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES";
export const LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS =
  "LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS";
export const LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL =
  "LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL";

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

export type UserAccountDepositBundlesActionCreators =
  | LoadUserAccountDepositMoneyBundlesActionCreator
  | LoadUserAccountDepositTokenBundlesActionCreator
  | LoadUserAccountWithdrawalMoneyBundlesActionCreator;
export type UserAccountDepositBundlesActions =
  | LoadUserAccountDepositMoneyBundlesAction
  | LoadUserAccountDepositTokenBundlesAction
  | LoadUserAccountWithdrawalMoneyBundlesAction;
export type UserAccountDepositBundlesState = AxiosState<Bundle[]>;

export function loadTransactionBundles(
  transactionType: TransactionType,
  currency: Currency
): UserAccountDepositBundlesActionCreators {
  const payload: AxiosPayload = {
    request: {
      method: "GET",
      url: `/cashier/api/transactions/${transactionType}/bundles`,
      params: {
        currency
      }
    }
  };
  switch (currency) {
    case MONEY: {
      switch (transactionType) {
        case TRANSACTION_TYPE_DEPOSIT: {
          return {
            types: [
              LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES,
              LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS,
              LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL
            ],
            payload
          };
        }
        case TRANSACTION_TYPE_WITHDRAWAL: {
          return {
            types: [
              LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES,
              LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS,
              LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL
            ],
            payload
          };
        }
      }
      break;
    }
    case TOKEN: {
      switch (transactionType) {
        case TRANSACTION_TYPE_DEPOSIT: {
          return {
            types: [
              LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES,
              LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS,
              LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL
            ],
            payload
          };
        }
      }
    }
  }
}
