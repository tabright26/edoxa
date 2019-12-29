import {
  LOAD_USER_MONEY_ACCOUNT_BALANCE,
  LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS,
  LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL,
  LOAD_USER_TOKEN_ACCOUNT_BALANCE,
  LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS,
  LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL,
  USER_ACCOUNT_DEPOSIT_MONEY,
  USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS,
  USER_ACCOUNT_DEPOSIT_MONEY_FAIL,
  USER_ACCOUNT_DEPOSIT_TOKEN,
  USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS,
  USER_ACCOUNT_DEPOSIT_TOKEN_FAIL,
  LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES,
  LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS,
  LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL,
  LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES,
  LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS,
  LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL,
  LOAD_USER_ACCOUNT_TRANSACTIONS,
  LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS,
  LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL,
  USER_ACCOUNT_WITHDRAWAL_MONEY,
  USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS,
  USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL,
  LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES,
  LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS,
  LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL,
  UserAccountDepositBundlesActionCreators,
  UserAccountDepositActionCreators,
  UserAccountWithdrawalActionCreators,
  UserAccountTransactionsActionCreators,
  UserAccountBalanceActionCreators
} from "./types";

import {
  Currency,
  CURRENCY_MONEY,
  CURRENCY_TOKEN,
  TransactionType,
  TransactionStatus,
  TRANSACTION_TYPE_DEPOSIT,
  TRANSACTION_TYPE_WITHDRAWAL
} from "types";
import { AxiosPayload, AxiosActionCreatorMeta } from "utils/axios/types";

export function loadUserAccountBalance(
  currency: Currency
): UserAccountBalanceActionCreators {
  const payload: AxiosPayload = {
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

export function accountDeposit(
  currency: Currency,
  amount: number,
  meta: AxiosActionCreatorMeta
): UserAccountDepositActionCreators {
  const payload: AxiosPayload = {
    request: {
      method: "POST",
      url: `/cashier/api/account/deposit/${currency}`,
      data: amount,
      headers: {
        "content-type": "application/json-patch+json"
      }
    }
  };
  switch (currency) {
    case CURRENCY_MONEY:
      return {
        types: [
          USER_ACCOUNT_DEPOSIT_MONEY,
          USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS,
          USER_ACCOUNT_DEPOSIT_MONEY_FAIL
        ],
        payload,
        meta
      };
    case CURRENCY_TOKEN:
      return {
        types: [
          USER_ACCOUNT_DEPOSIT_TOKEN,
          USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS,
          USER_ACCOUNT_DEPOSIT_TOKEN_FAIL
        ],
        payload,
        meta
      };
  }
}

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
    case CURRENCY_MONEY: {
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
    case CURRENCY_TOKEN: {
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

export function loadUserAccountTransactions(
  currency: Currency | null = null,
  type: TransactionType | null = null,
  status: TransactionStatus | null = null
): UserAccountTransactionsActionCreators {
  return {
    types: [
      LOAD_USER_ACCOUNT_TRANSACTIONS,
      LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS,
      LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL
    ],
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

export function accountWithdrawal(
  currency: Currency,
  amount: number,
  meta: AxiosActionCreatorMeta
): UserAccountWithdrawalActionCreators {
  const payload: AxiosPayload = {
    request: {
      method: "POST",
      url: `/cashier/api/account/withdrawal/${currency}`,
      data: amount,
      headers: {
        "Content-Type": "application/json-patch+json"
      }
    }
  };
  switch (currency) {
    case CURRENCY_MONEY:
      return {
        types: [
          USER_ACCOUNT_WITHDRAWAL_MONEY,
          USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS,
          USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL
        ],
        payload,
        meta
      };
    case CURRENCY_TOKEN:
      throw new Error("Token is not supported for withdrawal.");
  }
}