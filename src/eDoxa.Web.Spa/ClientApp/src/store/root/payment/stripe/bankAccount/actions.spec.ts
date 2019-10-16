import { LOAD_STRIPE_BANKACCOUNT, LOAD_STRIPE_BANKACCOUNT_SUCCESS, LOAD_STRIPE_BANKACCOUNT_FAIL } from "./types";
import { loadBankAccount } from "./actions";

describe("stripe actions", () => {
  it("should create an action to get user stripe banks", () => {
    const expectedType = [LOAD_STRIPE_BANKACCOUNT, LOAD_STRIPE_BANKACCOUNT_SUCCESS, LOAD_STRIPE_BANKACCOUNT_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/payment/api/stripe/bank-account";

    const actionCreator = loadBankAccount();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
