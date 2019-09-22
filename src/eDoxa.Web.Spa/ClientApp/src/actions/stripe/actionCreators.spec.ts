import { loadPaymentMethods, loadBankAccounts } from "./actionCreators";

describe("stripe actions", () => {
  it("should create an action to get user stripe cards", () => {
    const expectedCustomer = "cus_qwe12312eqw12";
    const expectedType = "card";
    const expectedTypes = ["LOAD_PAYMENTMETHODS", "LOAD_PAYMENTMETHODS_SUCCESS", "LOAD_PAYMENTMETHODS_FAIL"];
    const expectedClient = "stripe";
    const expectedMethod = "GET";
    const expectedUrl = `/v1/payment_methods?customer=${expectedCustomer}&type=${expectedType}`;

    const actionCreator = loadPaymentMethods(expectedCustomer, expectedType);

    expect(actionCreator.types).toEqual(expectedTypes);
    expect(actionCreator.payload.client).toEqual(expectedClient);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get user stripe banks", () => {
    const expectedType = ["LOAD_BANK_ACCOUNTS", "LOAD_BANK_ACCOUNTS_SUCCESS", "LOAD_BANK_ACCOUNTS_FAIL"];
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
