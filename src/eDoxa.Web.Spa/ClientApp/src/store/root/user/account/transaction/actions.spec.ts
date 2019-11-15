import { loadUserAccountTransactions } from "./actions";
import { LOAD_USER_ACCOUNT_TRANSACTIONS, LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL } from "./types";

describe("cashier actions", () => {
  it("should create an action to get user transactions", () => {
    const expectedCurrency = "token";
    const expectedType = [LOAD_USER_ACCOUNT_TRANSACTIONS, LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/cashier/api/transactions`;

    const actionCreator = loadUserAccountTransactions(expectedCurrency);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
