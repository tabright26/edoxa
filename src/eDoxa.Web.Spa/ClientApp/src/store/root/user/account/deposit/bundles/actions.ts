import {
  LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES,
  LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS,
  LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL,
  LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES,
  LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS,
  LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL,
  UserAccountDepositBundlesActionCreators
} from "./types";
import { Currency, MONEY, TOKEN } from "types";
import { AxiosPayload } from "store/middlewares/axios/types";

export function loadUserAccountDepositBundlesFor(currency: Currency): UserAccountDepositBundlesActionCreators {
  const payload: AxiosPayload = {
    request: {
      method: "GET",
      url: `/cashier/api/account/deposit/${currency}/bundles`
    }
  };
  switch (currency) {
    case MONEY:
      return {
        types: [LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES, LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS, LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL],
        payload
      };
    case TOKEN:
      return {
        types: [LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES, LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS, LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL],
        payload
      };
  }
}
