import { reducer, initialState } from "./reducer";

const stripeCard204Data = [];
const stripeCard200Data = { data: [{ id: "1" }] };

describe("user account stripe card reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_PAYMENTMETHODS_SUCCESS",
      payload: { status: 204, data: stripeCard204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_PAYMENTMETHODS_SUCCESS",
      payload: { status: 200, data: stripeCard200Data }
    };
    expect(reducer(initialState, action)).toEqual(stripeCard200Data);
  });

  it("should handle LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL", () => {
    const action: any = {
      type: "LOAD_PAYMENTMETHODS_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
