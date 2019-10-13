import { loadUserAccountBalance } from "./actions";
import { LOAD_USER_ACCOUNT_BALANCE, LOAD_USER_ACCOUNT_BALANCE_SUCCESS, LOAD_USER_ACCOUNT_BALANCE_FAIL } from "./types";

describe("cashier actions", () => {
  it("should create an action to get user balance money", () => {
    const expectedCurrency = "money";
    const expectedType = [LOAD_USER_ACCOUNT_BALANCE, LOAD_USER_ACCOUNT_BALANCE_SUCCESS, LOAD_USER_ACCOUNT_BALANCE_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/cashier/api/account/balance/${expectedCurrency}`;

    const actionCreator = loadUserAccountBalance(expectedCurrency);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
