import { loadUserAccountBalance, loadUserAccountTransactions } from "./creators";

describe("cashier actions", () => {
  it("should create an action to get user balance money", () => {
    const expectedCurrency = "money";
    const expectedType = ["LOAD_USER_ACCOUNT_BALANCE", "LOAD_USER_ACCOUNT_BALANCE_SUCCESS", "LOAD_USER_ACCOUNT_BALANCE_FAIL"];
    const expectedMethod = "GET";
    const expectedUrl = `/cashier/api/account/balance/${expectedCurrency}`;

    const actionCreator = loadUserAccountBalance(expectedCurrency);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get user transactions", () => {
    const expectedCurrency = "token";
    const expectedType = ["LOAD_USER_ACCOUNT_TRANSACTIONS", "LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS", "LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL"];
    const expectedMethod = "GET";
    const expectedUrl = `/cashier/api/transactions?currency=${expectedCurrency}`;

    const actionCreator = loadUserAccountTransactions(expectedCurrency);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
