import { LOAD_STRIPE_ACCOUNT, LOAD_STRIPE_ACCOUNT_SUCCESS, LOAD_STRIPE_ACCOUNT_FAIL, StripeAccountActionCreators } from "./types";

export function loadAccount(): StripeAccountActionCreators {
  return {
    types: [LOAD_STRIPE_ACCOUNT, LOAD_STRIPE_ACCOUNT_SUCCESS, LOAD_STRIPE_ACCOUNT_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/payment/api/stripe/account"
      }
    }
  };
}
