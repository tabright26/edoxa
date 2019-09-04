import { reducer } from "./userAccountTransactionsReducer";
import * as types from "../actions/cashierActions";

describe("user account transactions reducer", () => {
  it("should return the initial state", () => {
    expect(reducer([], {})).toEqual([]);
  });

  it("should handle LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS 204", () => {
    expect(
      reducer([], {
        type: types.LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS,
        payload: { status: 204, data: "Run the tests" }
      })
    ).toEqual([]);
  });

  it("should handle LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS Empty", () => {
    expect(
      reducer([], {
        type: types.LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS,
        payload: { status: {}, data: "Run the tests" }
      })
    ).toEqual("Run the tests");
  });

  it("should handle LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL", () => {
    expect(
      reducer([], {
        type: types.LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL,
        payload: { data: "This is an error message" }
      })
    ).toEqual([]);
  });
});
