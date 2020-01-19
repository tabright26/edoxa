import {
  LOAD_STRIPE_BANKACCOUNT,
  LOAD_STRIPE_BANKACCOUNT_SUCCESS,
  LOAD_STRIPE_BANKACCOUNT_FAIL,
  LOAD_STRIPE_PAYMENTMETHODS,
  LOAD_STRIPE_PAYMENTMETHODS_SUCCESS,
  LOAD_STRIPE_PAYMENTMETHODS_FAIL
} from "./types";

import { loadStripeBankAccount, loadStripePaymentMethods } from "./index";

describe("stripe actions", () => {
  it("should create an action to get user stripe banks", () => {
    const expectedType = [
      LOAD_STRIPE_BANKACCOUNT,
      LOAD_STRIPE_BANKACCOUNT_SUCCESS,
      LOAD_STRIPE_BANKACCOUNT_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = "/payment/api/stripe/bank-account";

    const actionCreator = loadStripeBankAccount();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});

describe("stripe actions", () => {
  it("should create an action to get user stripe cards", () => {
    const expectedTypes = [
      LOAD_STRIPE_PAYMENTMETHODS,
      LOAD_STRIPE_PAYMENTMETHODS_SUCCESS,
      LOAD_STRIPE_PAYMENTMETHODS_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = "/payment/api/stripe/payment-method";

    const actionCreator = loadStripePaymentMethods();

    expect(actionCreator.types).toEqual(expectedTypes);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
