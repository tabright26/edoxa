import { IAxiosActionCreator } from "interfaces/axios";

export const LOAD_USER_ACCOUNT_BALANCE = "LOAD_USER_ACCOUNT_BALANCE";
export const LOAD_USER_ACCOUNT_BALANCE_SUCCESS = "LOAD_USER_ACCOUNT_BALANCE_SUCCESS";
export const LOAD_USER_ACCOUNT_BALANCE_FAIL = "LOAD_USER_ACCOUNT_BALANCE_FAIL";
export type LoadUserAccountBalanceActionType = "LOAD_USER_ACCOUNT_BALANCE" | "LOAD_USER_ACCOUNT_BALANCE_SUCCESS" | "LOAD_USER_ACCOUNT_BALANCE_FAIL";
export function loadUserAccountBalance(currency: "money" | "token"): IAxiosActionCreator<LoadUserAccountBalanceActionType> {
  return {
    types: ["LOAD_USER_ACCOUNT_BALANCE", "LOAD_USER_ACCOUNT_BALANCE_SUCCESS", "LOAD_USER_ACCOUNT_BALANCE_FAIL"],
    payload: {
      request: {
        method: "GET",
        url: `/cashier/api/account/balance/${currency}`
      }
    }
  };
}

export const LOAD_USER_ACCOUNT_TRANSACTIONS = "LOAD_USER_ACCOUNT_TRANSACTIONS";
export const LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS = "LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS";
export const LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL = "LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL";
export type LoadUserAccountTransactionsActionType = "LOAD_USER_ACCOUNT_TRANSACTIONS" | "LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS" | "LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL";
export function loadUserAccountTransactions(currency: "money" | "token"): IAxiosActionCreator<LoadUserAccountTransactionsActionType> {
  return {
    types: ["LOAD_USER_ACCOUNT_TRANSACTIONS", "LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS", "LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL"],
    payload: {
      request: {
        method: "GET",
        url: `/cashier/api/transactions?currency=${currency}`
      }
    }
  };
}
