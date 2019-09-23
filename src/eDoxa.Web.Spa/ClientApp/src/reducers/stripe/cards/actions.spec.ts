import { loadPaymentMethods } from "./actions";
import { LOAD_PAYMENTMETHODS, LOAD_PAYMENTMETHODS_SUCCESS, LOAD_PAYMENTMETHODS_FAIL } from "./types";

describe("stripe actions", () => {
  it("should create an action to get user stripe cards", () => {
    const expectedCustomer = "cus_qwe12312eqw12";
    const expectedType = "card";
    const expectedTypes = [LOAD_PAYMENTMETHODS, LOAD_PAYMENTMETHODS_SUCCESS, LOAD_PAYMENTMETHODS_FAIL];
    const expectedClient = "stripe";
    const expectedMethod = "GET";
    const expectedUrl = `/v1/payment_methods?customer=${expectedCustomer}&type=${expectedType}`;

    const actionCreator = loadPaymentMethods(expectedCustomer, expectedType);

    expect(actionCreator.types).toEqual(expectedTypes);
    expect(actionCreator.payload.client).toEqual(expectedClient);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
