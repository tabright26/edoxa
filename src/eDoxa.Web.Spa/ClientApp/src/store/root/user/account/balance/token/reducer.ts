import { LOAD_USER_TOKEN_ACCOUNT_BALANCE, LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS, LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL, UserTokenAccountBalanceActions, UserTokenAccountBalanceState } from "./types";
import { Reducer } from "redux";

export const initialState: UserTokenAccountBalanceState = {
  data: {
    available: 0,
    pending: 0
  },
  error: null,
  loading: false
};

export const reducer: Reducer<UserTokenAccountBalanceState, UserTokenAccountBalanceActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_TOKEN_ACCOUNT_BALANCE: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS: {
      return { data: action.payload.data, error: null, loading: false };
    }
    case LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
