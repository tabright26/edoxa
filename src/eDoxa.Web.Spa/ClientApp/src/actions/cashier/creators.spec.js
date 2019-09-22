import { loadUserAccountBalance, loadUserAccountTransactions } from "./creators";
import actionTypes from "./index";

describe("cashier actions", () => {
  it("should create an action to get user balance money", () => {
    const expectedCurrency = "money";
    const expectedType = [actionTypes.LOAD_USER_ACCOUNT_BALANCE, actionTypes.LOAD_USER_ACCOUNT_BALANCE_SUCCESS, actionTypes.LOAD_USER_ACCOUNT_BALANCE_FAIL];
    const expectedMethod = "get";
    const expectedUrl = `/cashier/api/account/balance/${expectedCurrency}`;

    const actionCreator = loadUserAccountBalance(expectedCurrency);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get user transactions", () => {
    const expectedCurrency = "token";
    const expectedType = [actionTypes.LOAD_USER_ACCOUNT_TRANSACTIONS, actionTypes.LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, actionTypes.LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL];
    const expectedMethod = "get";
    const expectedUrl = `/cashier/api/transactions?currency=${expectedCurrency}`;

    const actionCreator = loadUserAccountTransactions(expectedCurrency);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
