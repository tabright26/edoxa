import { reducer, initialState } from "./reducer";

const transaction204Data = [];
const transaction200Data = [{ data: [{ id: "1" }] }];

describe("user account transactions reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS",
      payload: { status: 204, data: transaction204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS",
      payload: { status: 200, data: transaction200Data }
    };
    expect(reducer([], action)).toEqual(transaction200Data);
  });

  it("should handle LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL", () => {
    const action: any = {
      type: "LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
