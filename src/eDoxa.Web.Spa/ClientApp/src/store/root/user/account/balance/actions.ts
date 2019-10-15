import { LOAD_USER_ACCOUNT_BALANCE, LOAD_USER_ACCOUNT_BALANCE_SUCCESS, LOAD_USER_ACCOUNT_BALANCE_FAIL, BalanceActionCreators } from "./types";

export function loadUserAccountBalance(currency: "money" | "token"): BalanceActionCreators {
  return {
    types: [LOAD_USER_ACCOUNT_BALANCE, LOAD_USER_ACCOUNT_BALANCE_SUCCESS, LOAD_USER_ACCOUNT_BALANCE_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/cashier/api/account/balance/${currency}`
      }
    }
  };
}
