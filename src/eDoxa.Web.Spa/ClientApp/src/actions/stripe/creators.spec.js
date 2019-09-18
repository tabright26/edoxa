import { loadCards, loadBankAccounts } from "./creators";
import actionTypes from "./index";

describe("stripe actions", () => {
  it("should create an action to get user stripe cards", () => {
    const expectedType = [actionTypes.LOAD_CARDS, actionTypes.LOAD_CARDS_SUCCESS, actionTypes.LOAD_CARDS_FAIL];
    const expectedClient = "stripe";
    const expectedMethod = "get";
    const expectedUrl = `/v1/customers/:customerId/sources?object=card`;

    const actionCreator = loadCards();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.client).toEqual(expectedClient);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get user stripe banks", () => {
    const expectedType = [actionTypes.LOAD_BANK_ACCOUNTS, actionTypes.LOAD_BANK_ACCOUNTS_SUCCESS, actionTypes.LOAD_BANK_ACCOUNTS_FAIL];
    const expectedClient = "stripe";
    const expectedMethod = "get";
    const expectedUrl = "/v1/accounts/:connectAccountId/external_accounts?object=bank_account";

    const actionCreator = loadBankAccounts();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.client).toEqual(expectedClient);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
