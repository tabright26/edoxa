import { loadPaymentMethods } from "./actions";
import { LOAD_PAYMENTMETHODS, LOAD_PAYMENTMETHODS_SUCCESS, LOAD_PAYMENTMETHODS_FAIL, CARD_PAYMENTMETHOD_TYPE } from "./types";

describe("stripe actions", () => {
  it("should create an action to get user stripe cards", () => {
    const expectedType = CARD_PAYMENTMETHOD_TYPE;
    const expectedTypes = [LOAD_PAYMENTMETHODS, LOAD_PAYMENTMETHODS_SUCCESS, LOAD_PAYMENTMETHODS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/payment/api/stripe/payment-methods?type=${expectedType}`;

    const actionCreator = loadPaymentMethods(expectedType);

    expect(actionCreator.types).toEqual(expectedTypes);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
