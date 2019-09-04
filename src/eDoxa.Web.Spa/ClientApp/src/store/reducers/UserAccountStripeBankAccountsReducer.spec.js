import { reducer } from "./userAccountStripeBankAccountsReducer";
import * as types from "../actions/stripeActions";

const initialState = { data: [] };

describe("user account stripe bank account reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS 204", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS,
        payload: { status: 204, data: "Run the tests" }
      })
    ).toEqual(initialState);
  });

  it("should handle LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS Empty", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS,
        payload: { status: {}, data: "Run the tests" }
      })
    ).toEqual("Run the tests");
  });

  it("should handle LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL,
        payload: { data: "This is an error message" }
      })
    ).toEqual(initialState);
  });
});
