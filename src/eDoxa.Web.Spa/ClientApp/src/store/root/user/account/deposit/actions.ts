import {
  LOAD_DEPOSIT_MONEY_AMOUNTS,
  LOAD_DEPOSIT_MONEY_AMOUNTS_SUCCESS,
  LOAD_DEPOSIT_MONEY_AMOUNTS_FAIL,
  LOAD_DEPOSIT_TOKEN_AMOUNTS,
  LOAD_DEPOSIT_TOKEN_AMOUNTS_SUCCESS,
  LOAD_DEPOSIT_TOKEN_AMOUNTS_FAIL,
  DEPOSIT_MONEY,
  DEPOSIT_MONEY_SUCCESS,
  DEPOSIT_MONEY_FAIL,
  DEPOSIT_TOKEN,
  DEPOSIT_TOKEN_SUCCESS,
  DEPOSIT_TOKEN_FAIL,
  DepositActionCreators
} from "./types";
import { Currency } from "../types";

export function deposit(currency: Currency, amount: number): DepositActionCreators {
  switch (currency) {
    case "money":
      return {
        types: [DEPOSIT_MONEY, DEPOSIT_MONEY_SUCCESS, DEPOSIT_MONEY_FAIL],
        payload: {
          request: {
            method: "POST",
            url: `/cashier/api/account/deposit/${currency}`,
            data: amount,
            headers: {
              "content-type": "application/json-patch+json"
            }
          }
        }
      };
    case "token":
      return {
        types: [DEPOSIT_TOKEN, DEPOSIT_TOKEN_SUCCESS, DEPOSIT_TOKEN_FAIL],
        payload: {
          request: {
            method: "POST",
            url: `/cashier/api/account/deposit/${currency}`,
            data: amount,
            headers: {
              "Content-Type": "application/json-patch+json"
            }
          }
        }
      };
  }
}

export function loadDepositAmounts(currency: Currency): DepositActionCreators {
  switch (currency) {
    case "money":
      return {
        types: [LOAD_DEPOSIT_MONEY_AMOUNTS, LOAD_DEPOSIT_MONEY_AMOUNTS_SUCCESS, LOAD_DEPOSIT_MONEY_AMOUNTS_FAIL],
        payload: {
          request: {
            method: "GET",
            url: `/cashier/api/account/deposit/${currency}/amounts`
          }
        }
      };
    case "token":
      return {
        types: [LOAD_DEPOSIT_TOKEN_AMOUNTS, LOAD_DEPOSIT_TOKEN_AMOUNTS_SUCCESS, LOAD_DEPOSIT_TOKEN_AMOUNTS_FAIL],
        payload: {
          request: {
            method: "GET",
            url: `/cashier/api/account/deposit/${currency}/amounts`
          }
        }
      };
  }
}
