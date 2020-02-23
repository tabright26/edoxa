import {
  LOAD_USER_TRANSACTION_HISTORY,
  LOAD_USER_TRANSACTION_HISTORY_SUCCESS,
  LOAD_USER_TRANSACTION_HISTORY_FAIL,
  DEPOSIT_TRANSACTION,
  DEPOSIT_TRANSACTION_SUCCESS,
  DEPOSIT_TRANSACTION_FAIL,
  REDEEM_PROMOTION,
  REDEEM_PROMOTION_SUCCESS,
  REDEEM_PROMOTION_FAIL,
  DepositTransactionActionCreator,
  RedeemPromotionActionCreator,
  LoadUserTransactionHistoryActionCreator,
  WITHDRAW_TRANSACTION,
  WithdrawTransactionActionCreator,
  WITHDRAW_TRANSACTION_SUCCESS,
  WITHDRAW_TRANSACTION_FAIL
} from "./types";
import {
  CurrencyType,
  TransactionType,
  TransactionStatus,
  TransactionBundleId
} from "types/cashier";
import {
  AxiosActionCreatorMeta,
  AXIOS_PAYLOAD_CLIENT_CASHIER
} from "utils/axios/types";

export function depositTransaction(
  bundleId: TransactionBundleId,
  paymentMethodId: string = null,
  meta: AxiosActionCreatorMeta
): DepositTransactionActionCreator {
  return {
    types: [
      DEPOSIT_TRANSACTION,
      DEPOSIT_TRANSACTION_SUCCESS,
      DEPOSIT_TRANSACTION_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "POST",
        url: "/api/transactions/deposit",
        data: {
          bundleId,
          paymentMethodId
        }
      }
    },
    meta
  };
}

export function withdrawTransaction(
  bundleId: TransactionBundleId,
  email: string = null,
  meta: AxiosActionCreatorMeta
): WithdrawTransactionActionCreator {
  return {
    types: [
      WITHDRAW_TRANSACTION,
      WITHDRAW_TRANSACTION_SUCCESS,
      WITHDRAW_TRANSACTION_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "POST",
        url: "/api/transactions/withdraw",
        data: {
          bundleId,
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
