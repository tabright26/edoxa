import { loadUserTransactionHistory } from "./index";

import {
  LOAD_USER_TRANSACTION_HISTORY,
  LOAD_USER_TRANSACTION_HISTORY_SUCCESS,
  LOAD_USER_TRANSACTION_HISTORY_FAIL
} from "./types";

describe("cashier actions", () => {
  it("should create an action to get user transactions", () => {
    const expectedCurrency = "token";
    const expectedType = [
      LOAD_USER_TRANSACTION_HISTORY,
      LOAD_USER_TRANSACTION_HISTORY_SUCCESS,
      LOAD_USER_TRANSACTION_HISTORY_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = `/cashier/api/transactions`;

    const actionCreator = loadUserTransactionHistory(expectedCurrency);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
