import { LOAD_USER_ACCOUNT_TRANSACTIONS, LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL, TransactionsActionCreators } from "./types";

export function loadUserAccountTransactions(currency: "money" | "token"): TransactionsActionCreators {
  return {
    types: [LOAD_USER_ACCOUNT_TRANSACTIONS, LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/cashier/api/transactions?currency=${currency}`
      }
    }
  };
}
