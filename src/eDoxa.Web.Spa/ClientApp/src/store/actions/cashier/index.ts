import {
  LOAD_USER_MONEY_ACCOUNT_BALANCE,
  LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS,
  LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL,
  LOAD_USER_TOKEN_ACCOUNT_BALANCE,
  LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS,
  LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL,
  LOAD_TRANSACTION_BUNDLES,
  LOAD_TRANSACTION_BUNDLES_SUCCESS,
  LOAD_TRANSACTION_BUNDLES_FAIL,
  LOAD_USER_TRANSACTION_HISTORY,
  LOAD_USER_TRANSACTION_HISTORY_SUCCESS,
  LOAD_USER_TRANSACTION_HISTORY_FAIL,
  TransactionBundlesActionCreators,
  UserTransactionHistoryActionCreators as UserTransactionActionCreators,
  UserAccountBalanceActionCreators as UserBalanceActionCreators,
  CREATE_USER_TRANSACTION,
  CREATE_USER_TRANSACTION_SUCCESS,
  CREATE_USER_TRANSACTION_FAIL
} from "./types";
import {
  Currency,
  CURRENCY_MONEY,
  CURRENCY_TOKEN,
  TransactionType,
  TransactionStatus,
  TRANSACTION_TYPE_ALL,
  CURRENCY_ALL,
  TransactionBundleId
} from "types";
import {
  AxiosPayload,
  AxiosActionCreatorMeta,
  AXIOS_PAYLOAD_CLIENT_CASHIER
} from "utils/axios/types";

export function createUserTransaction(
  transactionBundleId: TransactionBundleId,
  meta: AxiosActionCreatorMeta
): UserTransactionActionCreators {
  return {
    types: [
      CREATE_USER_TRANSACTION,
      CREATE_USER_TRANSACTION_SUCCESS,
      CREATE_USER_TRANSACTION_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "POST",
        url: "/api/transactions",
        data: {
          bundle: transactionBundleId
        }
      }
    },
    meta
  };
}

export function loadUserBalance(currency: Currency): UserBalanceActionCreators {
  const payload: AxiosPayload = {
    client: AXIOS_PAYLOAD_CLIENT_CASHIER,
    request: {
      method: "GET",
      url: `/cashier/api/balance/${currency}`
    }
  };
  switch (currency) {
    case CURRENCY_MONEY: {
      return {
        types: [
          LOAD_USER_MONEY_ACCOUNT_BALANCE,
          LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS,
          LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL
        ],
        payload
      };
    }
    case CURRENCY_TOKEN: {
      return {
        types: [
          LOAD_USER_TOKEN_ACCOUNT_BALANCE,
          LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS,
          LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL
        ],
        payload
      };
    }
  }
}

export function loadTransactionBundles(
  transactionType: TransactionType = TRANSACTION_TYPE_ALL,
  currency: Currency = CURRENCY_ALL
): TransactionBundlesActionCreators {
  return {
    types: [
      LOAD_TRANSACTION_BUNDLES,
      LOAD_TRANSACTION_BUNDLES_SUCCESS,
      LOAD_TRANSACTION_BUNDLES_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "GET",
        url: "/cashier/api/transaction-bundles",
        params: {
          transactionType,
          currency
        }
      }
    }
  };
}

export function loadUserTransactionHistory(
  currency: Currency | null = null,
  type: TransactionType | null = null,
  status: TransactionStatus | null = null
): UserTransactionActionCreators {
  return {
    types: [
      LOAD_USER_TRANSACTION_HISTORY,
      LOAD_USER_TRANSACTION_HISTORY_SUCCESS,
      LOAD_USER_TRANSACTION_HISTORY_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
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
