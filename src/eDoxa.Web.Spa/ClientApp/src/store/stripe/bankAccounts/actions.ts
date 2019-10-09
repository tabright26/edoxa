import {
  LOAD_BANK_ACCOUNTS,
  LOAD_BANK_ACCOUNTS_SUCCESS,
  LOAD_BANK_ACCOUNTS_FAIL,
  CHANGE_BANK_ACCOUNT,
  CHANGE_BANK_ACCOUNT_SUCCESS,
  CHANGE_BANK_ACCOUNT_FAIL,
  BankAccountsActionCreators
} from "./types";

export function loadBankAccounts(): BankAccountsActionCreators {
  return {
    types: [LOAD_BANK_ACCOUNTS, LOAD_BANK_ACCOUNTS_SUCCESS, LOAD_BANK_ACCOUNTS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/payment/api/stripe/bank-accounts"
      }
    }
  };
}

export function changeBankAccount(token: string): BankAccountsActionCreators {
  return {
    types: [CHANGE_BANK_ACCOUNT, CHANGE_BANK_ACCOUNT_SUCCESS, CHANGE_BANK_ACCOUNT_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/payment/api/stripe/bank-accounts",
        data: {
          token
        }
      }
    }
  };
}
