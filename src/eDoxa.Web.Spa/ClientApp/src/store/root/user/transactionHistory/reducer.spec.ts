import { reducer, initialState } from "./reducer";
import { AxiosError } from "axios";
import {
  LOAD_USER_TRANSACTION_HISTORY_SUCCESS,
  LOAD_USER_TRANSACTION_HISTORY_FAIL
} from "store/actions/cashier/types";

const transaction204Data = [];
const transaction200Data = [{ data: [{ id: "1" }] }];

describe("user account transactions reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS 204", () => {
    const action: any = {
      type: LOAD_USER_TRANSACTION_HISTORY_SUCCESS,
      payload: { status: 204, data: transaction204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS 200", () => {
    const action: any = {
      type: LOAD_USER_TRANSACTION_HISTORY_SUCCESS,
      payload: { status: 200, data: transaction200Data }
    };
    const state = {
      data: transaction200Data,

      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: LOAD_USER_TRANSACTION_HISTORY_FAIL,
      error
    };
    const state = {
      data: initialState.data,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });
});
