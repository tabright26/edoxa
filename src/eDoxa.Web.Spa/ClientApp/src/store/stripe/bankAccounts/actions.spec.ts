import { LOAD_BANK_ACCOUNTS, LOAD_BANK_ACCOUNTS_SUCCESS, LOAD_BANK_ACCOUNTS_FAIL } from "./types";
import { loadBankAccounts } from "./actions";

describe("stripe actions", () => {
  it("should create an action to get user stripe banks", () => {
    const expectedType = [LOAD_BANK_ACCOUNTS, LOAD_BANK_ACCOUNTS_SUCCESS, LOAD_BANK_ACCOUNTS_FAIL];
    const expectedClient = "stripe";
    const expectedMethod = "GET";
    const expectedUrl = "/v1/accounts/:connectAccountId/external_accounts?object=bank_account";

    const actionCreator = loadBankAccounts();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.client).toEqual(expectedClient);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
