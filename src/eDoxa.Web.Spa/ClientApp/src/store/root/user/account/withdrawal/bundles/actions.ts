import { Currency } from "types";
import {
  LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES,
  LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS,
  LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL,
  UserAccountWithdrawalBundlesActionCreators
} from "./types";
import { AxiosPayload } from "utils/axios/types";

export function loadUserAccountWithdrawalBundlesFor(currency: Currency): UserAccountWithdrawalBundlesActionCreators {
  const payload: AxiosPayload = {
    request: {
      method: "GET",
      url: `/cashier/api/account/withdrawal/${currency}/bundles`
    }
  };
  switch (currency) {
    case "money":
      return {
        types: [LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES, LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS, LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL],
        payload
      };
    case "token": {
      throw new Error("Token is not supported for withdrawal.");
    }
  }
}
