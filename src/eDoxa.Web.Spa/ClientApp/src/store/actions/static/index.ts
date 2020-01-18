import {
  LOAD_IDENTITY_STATIC_OPTIONS,
  LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS,
  LOAD_IDENTITY_STATIC_OPTIONS_FAIL,
  LOAD_PAYMENT_STATIC_OPTIONS,
  LOAD_PAYMENT_STATIC_OPTIONS_SUCCESS,
  LOAD_PAYMENT_STATIC_OPTIONS_FAIL,
  StaticOptionsActionCreators,
  TransactionBundlesActionCreators,
  LOAD_TRANSACTION_BUNDLES,
  LOAD_TRANSACTION_BUNDLES_SUCCESS,
  LOAD_TRANSACTION_BUNDLES_FAIL
} from "./types";
import {
  AXIOS_PAYLOAD_CLIENT_DEFAULT,
  AXIOS_PAYLOAD_CLIENT_CASHIER
} from "utils/axios/types";
import {
  TransactionType,
  TRANSACTION_TYPE_ALL,
  Currency,
  CURRENCY_ALL
} from "types";

export function loadIdentityStaticOptions(): StaticOptionsActionCreators {
  return {
    types: [
      LOAD_IDENTITY_STATIC_OPTIONS,
      LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS,
      LOAD_IDENTITY_STATIC_OPTIONS_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: `identity/api/static/options`
      }
    }
  };
}

export function loadPaymentStaticOptions(): StaticOptionsActionCreators {
  return {
    types: [
      LOAD_PAYMENT_STATIC_OPTIONS,
      LOAD_PAYMENT_STATIC_OPTIONS_SUCCESS,
      LOAD_PAYMENT_STATIC_OPTIONS_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "GET",
        url: `payment/api/static/options`
      }
    }
  };
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
