import {
  LOAD_WITHDRAWAL_TOKEN_AMOUNTS,
  LOAD_WITHDRAWAL_TOKEN_AMOUNTS_SUCCESS,
  LOAD_WITHDRAWAL_TOKEN_AMOUNTS_FAIL,
  WITHDRAWAL_TOKEN,
  WITHDRAWAL_TOKEN_SUCCESS,
  WITHDRAWAL_TOKEN_FAIL,
  WithdrawalActionCreators
} from "./types";
import { Currency } from "../types";

export function withdrawal(currency: Currency, amount: number): WithdrawalActionCreators {
  switch (currency) {
    case "money":
      throw new Error("Money is not supported for withdrawal.");
    case "token":
      return {
        types: [WITHDRAWAL_TOKEN, WITHDRAWAL_TOKEN_SUCCESS, WITHDRAWAL_TOKEN_FAIL],
        payload: {
          request: {
            method: "POST",
            url: `/cashier/api/account/withdrawal/${currency}`,
            data: amount
          }
        }
      };
  }
}

export function loadWithdrawalAmounts(currency: Currency): WithdrawalActionCreators {
  switch (currency) {
    case "money":
      throw new Error("Money is not supported for withdrawal.");
    case "token":
      return {
        types: [LOAD_WITHDRAWAL_TOKEN_AMOUNTS, LOAD_WITHDRAWAL_TOKEN_AMOUNTS_SUCCESS, LOAD_WITHDRAWAL_TOKEN_AMOUNTS_FAIL],
        payload: {
          request: {
            method: "GET",
            url: `/cashier/api/account/withdrawal/${currency}/amounts`
          }
        }
      };
  }
}
