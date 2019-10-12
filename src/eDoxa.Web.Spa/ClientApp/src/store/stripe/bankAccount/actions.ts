import { LOAD_BANK_ACCOUNT, LOAD_BANK_ACCOUNT_SUCCESS, LOAD_BANK_ACCOUNT_FAIL, UPDATE_BANK_ACCOUNT, UPDATE_BANK_ACCOUNT_SUCCESS, UPDATE_BANK_ACCOUNT_FAIL, BankAccountActionCreators } from "./types";

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

export function updateBankAccount(token: string): BankAccountActionCreators {
  return {
    types: [UPDATE_BANK_ACCOUNT, UPDATE_BANK_ACCOUNT_SUCCESS, UPDATE_BANK_ACCOUNT_FAIL],
    payload: {
      request: {
        method: "PUT",
        url: "/payment/api/stripe/bank-account",
        data: {
          token
        }
      }
    }
  };
}
