import { LOAD_BANK_ACCOUNT, LOAD_BANK_ACCOUNT_SUCCESS, LOAD_BANK_ACCOUNT_FAIL, CHANGE_BANK_ACCOUNT, CHANGE_BANK_ACCOUNT_SUCCESS, CHANGE_BANK_ACCOUNT_FAIL, BankAccountActionCreators } from "./types";

export function loadBankAccount(): BankAccountActionCreators {
  return {
    types: [LOAD_BANK_ACCOUNT, LOAD_BANK_ACCOUNT_SUCCESS, LOAD_BANK_ACCOUNT_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/payment/api/stripe/bank-account"
      }
    }
  };
}

export function changeBankAccount(token: string): BankAccountActionCreators {
  return {
    types: [CHANGE_BANK_ACCOUNT, CHANGE_BANK_ACCOUNT_SUCCESS, CHANGE_BANK_ACCOUNT_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/payment/api/stripe/bank-account",
        data: {
          token
        }
      }
    }
  };
}
