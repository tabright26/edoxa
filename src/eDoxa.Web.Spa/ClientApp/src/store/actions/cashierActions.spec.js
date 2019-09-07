import {
  loadUserAccountBalanceForMoney,
  loadUserAccountBalanceForToken,
  loadUserAccountTransactions,
  LOAD_USER_ACCOUNT_BALANCE_MONEY,
  LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS,
  LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL,
  LOAD_USER_ACCOUNT_BALANCE_TOKEN,
  LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS,
  LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL,
  LOAD_USER_ACCOUNT_TRANSACTIONS,
  LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS,
  LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL
} from "./cashierActions";

describe("cashier actions", () => {
  it("should create an action to get user balance money", () => {
    const expectedType = [LOAD_USER_ACCOUNT_BALANCE_MONEY, LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS, LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL];
    const expectedMethod = "get";
    const expectedUrl = "/cashier/api/account/balance/money";

    const object = loadUserAccountBalanceForMoney();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get user balance token", () => {
    const expectedType = [LOAD_USER_ACCOUNT_BALANCE_TOKEN, LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS, LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL];
    const expectedMethod = "get";
    const expectedUrl = "/cashier/api/account/balance/token";

    const object = loadUserAccountBalanceForToken();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get user transactions", () => {
    const expectedType = [LOAD_USER_ACCOUNT_TRANSACTIONS, LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL];
    const expectedMethod = "get";
    const expectedUrl = "/cashier/api/transactions";

    const object = loadUserAccountTransactions();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });
});
