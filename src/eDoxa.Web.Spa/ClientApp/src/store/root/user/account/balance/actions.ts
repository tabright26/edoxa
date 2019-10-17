import { LOAD_USER_MONEY_ACCOUNT_BALANCE, LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS, LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL } from "./money/types";
import { LOAD_USER_TOKEN_ACCOUNT_BALANCE, LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS, LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL } from "./token/types";
import { UserAccountBalanceActionCreators } from "./types";
import { Currency, MONEY_CURRENCY, TOKEN_CURRENCY } from "types";
import { AxiosPayload } from "store/middlewares/axios/types";

export function loadUserAccountBalance(currency: Currency): UserAccountBalanceActionCreators {
  const payload: AxiosPayload = {
    request: {
      method: "GET",
      url: `/cashier/api/account/balance/${currency}`
    }
  };
  switch (currency) {
    case MONEY_CURRENCY: {
      return {
        types: [LOAD_USER_MONEY_ACCOUNT_BALANCE, LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS, LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL],
        payload
      };
    }
    case TOKEN_CURRENCY: {
      return {
        types: [LOAD_USER_TOKEN_ACCOUNT_BALANCE, LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS, LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL],
        payload
      };
    }
  }
}
