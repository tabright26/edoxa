import { reducer, initialState } from "./reducer";
import { AxiosError } from "axios";
import {
  UPDATE_STRIPE_BANKACCOUNT_SUCCESS,
  LOAD_STRIPE_BANKACCOUNT_FAIL
} from "store/actions/payment/types";

const stripeBank204Data = {};

describe("user account stripe bank account reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS 204", () => {
    const action: any = {
      type: UPDATE_STRIPE_BANKACCOUNT_SUCCESS,
      payload: { status: 204, data: stripeBank204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: LOAD_STRIPE_BANKACCOUNT_FAIL,
      error
    };
    const state = {
      data: initialState.data,
      error,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });
});
