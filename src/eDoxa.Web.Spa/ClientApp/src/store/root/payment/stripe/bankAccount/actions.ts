import {
  LOAD_STRIPE_BANKACCOUNT,
  LOAD_STRIPE_BANKACCOUNT_SUCCESS,
  LOAD_STRIPE_BANKACCOUNT_FAIL,
  UPDATE_STRIPE_BANKACCOUNT,
  UPDATE_STRIPE_BANKACCOUNT_SUCCESS,
  UPDATE_STRIPE_BANKACCOUNT_FAIL,
  StripeBankAccountActionCreators
} from "./types";

export function loadStripeBankAccount(): StripeBankAccountActionCreators {
  return {
    types: [LOAD_STRIPE_BANKACCOUNT, LOAD_STRIPE_BANKACCOUNT_SUCCESS, LOAD_STRIPE_BANKACCOUNT_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/payment/api/stripe/bank-account"
      }
    }
  };
}

export function updateStripeBankAccount(token: string): StripeBankAccountActionCreators {
  return {
    types: [UPDATE_STRIPE_BANKACCOUNT, UPDATE_STRIPE_BANKACCOUNT_SUCCESS, UPDATE_STRIPE_BANKACCOUNT_FAIL],
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
