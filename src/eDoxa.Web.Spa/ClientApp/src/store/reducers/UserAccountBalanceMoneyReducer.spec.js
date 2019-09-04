import { reducer } from "./userAccountBalanceMoneyReducer";
import * as types from "../actions/cashierActions";

const initialState = { type: "Money", available: 0, pending: 0 };

describe("user account balance money reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS,
        payload: { data: { currency: "Money", available: 10, pending: 50 } }
      })
    ).toEqual({ type: "Money", available: 10, pending: 50 });
  });

  it("should handle LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL,
        payload: { data: "This is an error message" }
      })
    ).toEqual(initialState);
  });
});
