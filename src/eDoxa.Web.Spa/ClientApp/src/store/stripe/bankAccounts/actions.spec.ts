import { LOAD_BANK_ACCOUNTS, LOAD_BANK_ACCOUNTS_SUCCESS, LOAD_BANK_ACCOUNTS_FAIL } from "./types";
import { loadBankAccounts } from "./actions";

describe("stripe actions", () => {
  it("should create an action to get user stripe banks", () => {
    const expectedType = [LOAD_BANK_ACCOUNTS, LOAD_BANK_ACCOUNTS_SUCCESS, LOAD_BANK_ACCOUNTS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/payment/api/stripe/bank-accounts";

    const actionCreator = loadBankAccounts();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
