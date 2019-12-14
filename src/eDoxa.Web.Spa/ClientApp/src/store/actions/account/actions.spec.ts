import { loadUserAccountBalance } from "./actions";

import {
  LOAD_USER_MONEY_ACCOUNT_BALANCE,
  LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS,
  LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL,
  LOAD_USER_TOKEN_ACCOUNT_BALANCE,
  LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS,
  LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL
} from "./types";

import { MONEY, TOKEN } from "types";

describe("cashier actions", () => {
  it("should create an action to get user balance money", () => {
    const expectedCurrency = MONEY;
    const expectedType = [
      LOAD_USER_MONEY_ACCOUNT_BALANCE,
      LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS,
      LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = `/cashier/api/account/balance/${expectedCurrency}`;

    const actionCreator = loadUserAccountBalance(expectedCurrency);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get user balance money", () => {
    const expectedCurrency = TOKEN;
    const expectedType = [
      LOAD_USER_TOKEN_ACCOUNT_BALANCE,
      LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS,
      LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = `/cashier/api/account/balance/${expectedCurrency}`;

    const actionCreator = loadUserAccountBalance(expectedCurrency);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});

import { loadUserAccountTransactions } from "./actions";
import {
  LOAD_USER_ACCOUNT_TRANSACTIONS,
  LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS,
  LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL
} from "./types";

describe("cashier actions", () => {
  it("should create an action to get user transactions", () => {
    const expectedCurrency = "token";
    const expectedType = [
      LOAD_USER_ACCOUNT_TRANSACTIONS,
      LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS,
      LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = `/cashier/api/transactions`;

    const actionCreator = loadUserAccountTransactions(expectedCurrency);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
