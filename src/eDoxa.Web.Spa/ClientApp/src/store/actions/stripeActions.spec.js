import {
  loadUserStripeCards,
  loadUserStripeBankAccounts,
  LOAD_USER_STRIPE_CARDS,
  LOAD_USER_STRIPE_CARDS_SUCCESS,
  LOAD_USER_STRIPE_CARDS_FAIL,
  LOAD_USER_STRIPE_BANK_ACCOUNTS,
  LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS,
  LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL
} from "./stripeActions";

describe("stripe actions", () => {
  it("should create an action to get user stripe cards", () => {
    const expectedType = [LOAD_USER_STRIPE_CARDS, LOAD_USER_STRIPE_CARDS_SUCCESS, LOAD_USER_STRIPE_CARDS_FAIL];
    const expectedClient = "stripe";
    const expectedMethod = "get";
    const expectedUrl = `/v1/customers/:customerId/sources?object=card`;

    const object = loadUserStripeCards();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.client).toEqual(expectedClient);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get user stripe banks", () => {
    const expectedType = [LOAD_USER_STRIPE_BANK_ACCOUNTS, LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS, LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL];
    const expectedClient = "stripe";
    const expectedMethod = "get";
    const expectedUrl = "/v1/accounts/:connectAccountId/external_accounts?object=bank_account";

    const object = loadUserStripeBankAccounts();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.client).toEqual(expectedClient);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });
});
