import {
  USER_ACCOUNT_DEPOSIT_MONEY,
  USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS,
  USER_ACCOUNT_DEPOSIT_MONEY_FAIL,
  USER_ACCOUNT_DEPOSIT_TOKEN,
  USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS,
  USER_ACCOUNT_DEPOSIT_TOKEN_FAIL,
  UserAccountDepositActionCreators
} from "./types";
import { Currency, MONEY, TOKEN } from "types";
import { AxiosPayload } from "utils/axios/types";

export function accountDeposit(
  currency: Currency,
  amount: number,
  meta: any
): UserAccountDepositActionCreators {
  const payload: AxiosPayload = {
    request: {
      method: "POST",
      url: `/cashier/api/account/deposit/${currency}`,
      data: amount,
      headers: {
        "content-type": "application/json-patch+json"
      }
    }
  };
  switch (currency) {
    case MONEY:
      return {
        types: [
          USER_ACCOUNT_DEPOSIT_MONEY,
          USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS,
          USER_ACCOUNT_DEPOSIT_MONEY_FAIL
        ],
        payload,
        meta
      };
    case TOKEN:
      return {
        types: [
          USER_ACCOUNT_DEPOSIT_TOKEN,
          USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS,
          USER_ACCOUNT_DEPOSIT_TOKEN_FAIL
        ],
        payload,
        meta
      };
  }
}
