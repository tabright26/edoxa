import {
  LOAD_WITHDRAWAL_MONEY_AMOUNTS,
  LOAD_WITHDRAWAL_MONEY_AMOUNTS_SUCCESS,
  LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL,
  WITHDRAWAL_TOKEN,
  WITHDRAWAL_TOKEN_SUCCESS,
  WITHDRAWAL_TOKEN_FAIL,
  WithdrawalActionCreators
} from "./types";
import { Currency } from "../types";

export function withdrawal(currency: Currency, amount: number): WithdrawalActionCreators {
  switch (currency) {
    case "money":
      return {
        types: [WITHDRAWAL_TOKEN, WITHDRAWAL_TOKEN_SUCCESS, WITHDRAWAL_TOKEN_FAIL],
        payload: {
          request: {
            method: "POST",
            url: `/cashier/api/account/withdrawal/${currency}`,
            data: amount,
            headers: {
              "Content-Type": "application/json-patch+json"
            }
          }
        }
      };

    case "token":
      throw new Error("Token is not supported for withdrawal.");
  }
}

export function loadWithdrawalAmounts(currency: Currency): WithdrawalActionCreators {
  switch (currency) {
    case "money":
      return {
        types: [LOAD_WITHDRAWAL_MONEY_AMOUNTS, LOAD_WITHDRAWAL_MONEY_AMOUNTS_SUCCESS, LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL],
        payload: {
          request: {
            method: "GET",
            url: `/cashier/api/account/withdrawal/${currency}/amounts`
          }
        }
      };
    case "token":
      throw new Error("Token is not supported for withdrawal.");
  }
}
