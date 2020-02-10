import {
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
  LoadUserTransactionHistoryActionCreator
} from "./types";
import {
  CurrencyType,
  TransactionType,
  TransactionStatus,
  TransactionBundleId
} from "types";
import {
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

export function loadUserTransactionHistory(
  currencyType: CurrencyType = null,
  type: TransactionType = null,
  status: TransactionStatus = null
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
