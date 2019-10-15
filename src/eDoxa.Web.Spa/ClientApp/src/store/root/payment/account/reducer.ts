import { LOAD_ACCOUNT, LOAD_ACCOUNT_SUCCESS, LOAD_ACCOUNT_FAIL, AccountActionTypes, AccountState } from "./types";
import { Reducer } from "redux";

export const initialState: AccountState = {
  data: {},
  error: null,
  loading: false
};

export const reducer: Reducer<AccountState, AccountActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_ACCOUNT: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_ACCOUNT_SUCCESS: {
      return { data: action.payload.data, error: null, loading: false };
    }
    case LOAD_ACCOUNT_FAIL: {
      return { data: state.data, error: LOAD_ACCOUNT_FAIL, loading: false };
    }
    default: {
      return state;
    }
  }
};
