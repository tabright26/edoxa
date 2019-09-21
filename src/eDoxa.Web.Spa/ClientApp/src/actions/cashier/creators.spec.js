import { loadUserAccountBalance, loadUserAccountBalanceForToken, loadUserAccountTransactions } from "./creators";
import actionTypes from "./index";

describe("cashier actions", () => {
  it("should create an action to get user balance money", () => {
    const expectedType = [actionTypes.LOAD_USER_ACCOUNT_BALANCE_MONEY, actionTypes.LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS, actionTypes.LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL];
    const expectedMethod = "get";
    const expectedUrl = "/cashier/api/account/balance/money";

    const actionCreator = loadUserAccountBalance();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get user balance token", () => {
    const expectedType = [actionTypes.LOAD_USER_ACCOUNT_BALANCE_TOKEN, actionTypes.LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS, actionTypes.LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL];
    const expectedMethod = "get";
    const expectedUrl = "/cashier/api/account/balance/token";

    const actionCreator = loadUserAccountBalanceForToken();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get user transactions", () => {
    const expectedType = [actionTypes.LOAD_USER_ACCOUNT_TRANSACTIONS, actionTypes.LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, actionTypes.LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL];
    const expectedMethod = "get";
    const expectedUrl = "/cashier/api/transactions";

    const actionCreator = loadUserAccountTransactions();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
