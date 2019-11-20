import { LOAD_USER_ACCOUNT_TRANSACTIONS, LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL, UserAccountTransactionsActionCreators } from "./types";
import { Currency, TransactionType, TransactionStatus } from "types";

export function loadUserAccountTransactions(currency: Currency | null = null, type: TransactionType | null = null, status: TransactionStatus | null = null): UserAccountTransactionsActionCreators {
  return {
    types: [LOAD_USER_ACCOUNT_TRANSACTIONS, LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/cashier/api/transactions",
        params: {
          currency,
          type,
          status
        }
      }
    }
  };
}
