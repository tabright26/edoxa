import { reducer, initialState } from "./reducer";
import actionTypes from "actions/stripe";

const stripeBank204Data = [];
const stripeBaml200Data = { data: [{ id: "1" }] };

describe("user account stripe bank account reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS 204", () => {
    expect(
      reducer(initialState, {
        type: actionTypes.LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS,
        payload: { status: 204, data: stripeBank204Data }
      })
    ).toEqual(initialState);
  });

  it("should handle LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS 200", () => {
    expect(
      reducer(initialState, {
        type: actionTypes.LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS,
        payload: { status: 200, data: stripeBaml200Data }
      })
    ).toEqual(stripeBaml200Data);
  });

  it("should handle LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL", () => {
    expect(
      reducer(initialState, {
        type: actionTypes.LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL
      })
    ).toEqual(initialState);
  });
});
