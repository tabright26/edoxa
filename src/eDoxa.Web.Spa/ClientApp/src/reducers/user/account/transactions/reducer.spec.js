import { reducer, initialState } from "./reducer";
import actionTypes from "actions/cashier";

const transaction204Data = [];
const transaction200Data = [{ data: [{ id: "1" }] }];

describe("user account transactions reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS 204", () => {
    expect(
      reducer(initialState, {
        type: actionTypes.LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS,
        payload: { status: 204, data: transaction204Data }
      })
    ).toEqual(initialState);
  });

  it("should handle LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS 200", () => {
    expect(
      reducer([], {
        type: actionTypes.LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS,
        payload: { status: 200, data: transaction200Data }
      })
    ).toEqual(transaction200Data);
  });

  it("should handle LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL", () => {
    expect(
      reducer(initialState, {
        type: actionTypes.LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL
      })
    ).toEqual(initialState);
  });
});
