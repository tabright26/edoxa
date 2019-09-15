import { reducer, initialState } from "./reducer";
import actionTypes from "actions/cashier";

const successData = { currency: "Token", available: 10, pending: 50 };
const expectedState = { type: "Token", available: 10, pending: 50 };

describe("user account balance token reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS", () => {
    expect(
      reducer(initialState, {
        type: actionTypes.LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS,
        payload: { data: successData }
      })
    ).toEqual(expectedState);
  });

  it("should handle LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL", () => {
    expect(
      reducer(initialState, {
        type: actionTypes.LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL
      })
    ).toEqual(initialState);
  });
});
