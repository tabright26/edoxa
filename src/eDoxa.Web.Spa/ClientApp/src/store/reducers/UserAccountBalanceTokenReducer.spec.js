import { reducer } from "./userAccountBalanceTokenReducer";
import * as types from "../actions/cashierActions";

const initialState = { type: "Token", available: 0, pending: 0 };

describe("user account balance token reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS,
        payload: { data: { currency: "Token", available: 10, pending: 50 } }
      })
    ).toEqual({ type: "Token", available: 10, pending: 50 });
  });

  it("should handle LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL,
        payload: { data: "This is an error message" }
      })
    ).toEqual(initialState);
  });
});
