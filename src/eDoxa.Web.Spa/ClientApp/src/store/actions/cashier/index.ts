import {
  LOAD_USER_MONEY_ACCOUNT_BALANCE,
  LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS,
  LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL,
  LOAD_USER_TOKEN_ACCOUNT_BALANCE,
  LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS,
  LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL,
  LOAD_USER_TRANSACTION_HISTORY,
  LOAD_USER_TRANSACTION_HISTORY_SUCCESS,
  LOAD_USER_TRANSACTION_HISTORY_FAIL,
  UserTransactionActionCreators,
  UserAccountBalanceActionCreators,
  CREATE_USER_TRANSACTION,
  CREATE_USER_TRANSACTION_SUCCESS,
  CREATE_USER_TRANSACTION_FAIL,
  REDEEM_PROMOTION,
  REDEEM_PROMOTION_SUCCESS,
  REDEEM_PROMOTION_FAIL
} from "./types";
import {
  Currency,
  CURRENCY_MONEY,
  CURRENCY_TOKEN,
  TransactionType,
  TransactionStatus,
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

export function redeemPromotion(
  promotionalCode: string,
  meta: AxiosActionCreatorMeta
): UserTransactionActionCreators {
  return {
    types: [
      REDEEM_PROMOTION,
      REDEEM_PROMOTION_SUCCESS,
      REDEEM_PROMOTION_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "POST",
        url: `/cashier/api/promotions/${promotionalCode}`
      }
    },
    meta
  };
}

export function loadUserBalance(
  currency: Currency
): UserAccountBalanceActionCreators {
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
