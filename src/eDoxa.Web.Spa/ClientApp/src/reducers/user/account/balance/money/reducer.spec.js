import { reducer, initialState } from "./reducer";
import * as types from "../../../../../actions/cashierActions";

const successData = { currency: "Money", available: 10, pending: 50 };
const expectedState = { type: "Money", available: 10, pending: 50 };

describe("user account balance money reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS,
        payload: { data: successData }
      })
    ).toEqual(expectedState);
  });

  it("should handle LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL
      })
    ).toEqual(initialState);
  });
});
