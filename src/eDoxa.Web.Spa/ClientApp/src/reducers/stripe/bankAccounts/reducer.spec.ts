import { reducer, initialState } from "./reducer";

const stripeBank204Data = [];
const stripeBaml200Data = { data: [{ id: "1" }] };

describe("user account stripe bank account reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_BANK_ACCOUNTS_SUCCESS",
      payload: { status: 204, data: stripeBank204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_BANK_ACCOUNTS_SUCCESS",
      payload: { status: 200, data: stripeBaml200Data }
    };
    expect(reducer(initialState, action)).toEqual(stripeBaml200Data);
  });

  it("should handle LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL", () => {
    const action: any = {
      type: "LOAD_BANK_ACCOUNTS_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
