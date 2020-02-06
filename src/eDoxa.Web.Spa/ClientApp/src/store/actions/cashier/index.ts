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
  CREATE_USER_TRANSACTION,
  CREATE_USER_TRANSACTION_SUCCESS,
  CREATE_USER_TRANSACTION_FAIL,
  REDEEM_PROMOTION,
  REDEEM_PROMOTION_SUCCESS,
  REDEEM_PROMOTION_FAIL,
  CreateUserTransactionActionCreator,
  RedeemPromotionActionCreator,
  LoadUserMoneyAccountBalanceActionCreator,
  LoadUserTokenAccountBalanceActionCreator,
  LoadUserTransactionHistoryActionCreator
} from "./types";
import {
  CurrencyType,
  CURRENCY_TYPE_MONEY,
  CURRENCY_TYPE_TOKEN,
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
  bundleId: TransactionBundleId,
  email: string = null,
  meta: AxiosActionCreatorMeta
): CreateUserTransactionActionCreator {
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
          bundle: bundleId,
          email
        }
      }
    },
    meta
  };
}

export function redeemPromotion(
  promotionalCode: string,
  meta: AxiosActionCreatorMeta
): RedeemPromotionActionCreator {
  return {
    types: [REDEEM_PROMOTION, REDEEM_PROMOTION_SUCCESS, REDEEM_PROMOTION_FAIL],
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
  currencyType: CurrencyType
):
  | LoadUserMoneyAccountBalanceActionCreator
  | LoadUserTokenAccountBalanceActionCreator {
  const payload: AxiosPayload = {
    client: AXIOS_PAYLOAD_CLIENT_CASHIER,
    request: {
      method: "GET",
      url: `/cashier/api/balance/${currencyType}`
    }
  };
  switch (currencyType) {
    case CURRENCY_TYPE_MONEY: {
      return {
        types: [
          LOAD_USER_MONEY_ACCOUNT_BALANCE,
          LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS,
          LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL
        ],
        payload
      };
    }
    case CURRENCY_TYPE_TOKEN: {
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
  currencyType: CurrencyType | null = null,
  type: TransactionType | null = null,
  status: TransactionStatus | null = null
): LoadUserTransactionHistoryActionCreator {
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
          currencyType,
          type,
          status
        }
      }
    }
  };
}
