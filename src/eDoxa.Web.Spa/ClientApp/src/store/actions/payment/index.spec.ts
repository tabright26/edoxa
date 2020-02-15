import {
  LOAD_STRIPE_PAYMENTMETHODS,
  LOAD_STRIPE_PAYMENTMETHODS_SUCCESS,
  LOAD_STRIPE_PAYMENTMETHODS_FAIL
} from "./types";
import { loadStripePaymentMethods } from "./index";

describe("stripe actions", () => {
  it("should create an action to get user stripe cards", () => {
    const expectedTypes = [
      LOAD_STRIPE_PAYMENTMETHODS,
      LOAD_STRIPE_PAYMENTMETHODS_SUCCESS,
      LOAD_STRIPE_PAYMENTMETHODS_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = "/payment/api/stripe/payment-methods";

    const actionCreator = loadStripePaymentMethods();

    expect(actionCreator.types).toEqual(expectedTypes);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
