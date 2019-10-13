import { LOAD_ACCOUNT, LOAD_ACCOUNT_SUCCESS, LOAD_ACCOUNT_FAIL, AccountActionCreators } from "./types";

export function loadAccount(): AccountActionCreators {
  return {
    types: [LOAD_ACCOUNT, LOAD_ACCOUNT_SUCCESS, LOAD_ACCOUNT_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/payment/api/stripe/account"
      }
    }
  };
}
