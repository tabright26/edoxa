import { reducer, initialState } from "./userAccountStripeCardsReducer";
import * as types from "../actions/stripeActions";

const stripeCard204Data = [];
const stripeCard200Data = { data: [{ id: "1" }] };

describe("user account stripe card reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS 204", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_USER_STRIPE_CARDS_SUCCESS,
        payload: { status: 204, data: stripeCard204Data }
      })
    ).toEqual(initialState);
  });

  it("should handle LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS 200", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_USER_STRIPE_CARDS_SUCCESS,
        payload: { status: 200, data: stripeCard200Data }
      })
    ).toEqual(stripeCard200Data);
  });

  it("should handle LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_USER_STRIPE_CARDS_FAIL
      })
    ).toEqual(initialState);
  });
});
