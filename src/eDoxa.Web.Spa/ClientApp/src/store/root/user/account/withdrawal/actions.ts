import {
  USER_ACCOUNT_WITHDRAWAL_MONEY,
  USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS,
  USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL,
  UserAccountWithdrawalActionCreators
} from "./types";
import { Currency, MONEY, TOKEN } from "types";
import { AxiosPayload } from "utils/axios/types";

export function accountWithdrawal(
  currency: Currency,
  amount: number,
  meta: any
): UserAccountWithdrawalActionCreators {
  const payload: AxiosPayload = {
    request: {
      method: "POST",
      url: `/cashier/api/account/withdrawal/${currency}`,
      data: amount,
      headers: {
        "Content-Type": "application/json-patch+json"
      }
    }
  };
  switch (currency) {
    case MONEY:
      return {
        types: [
          USER_ACCOUNT_WITHDRAWAL_MONEY,
          USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS,
          USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL
        ],
        payload,
        meta
      };
    case TOKEN:
      throw new Error("Token is not supported for withdrawal.");
  }
}
