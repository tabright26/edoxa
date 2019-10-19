import {
  LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES,
  LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS,
  LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL,
  UserAccountWithdrawalBundlesState,
  UserAccountWithdrawalBundlesActions
} from "../types";
import { Reducer } from "redux";

export const initialState: UserAccountWithdrawalBundlesState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<UserAccountWithdrawalBundlesState, UserAccountWithdrawalBundlesActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS: {
      return { data: action.payload.data, error: null, loading: false };
    }
    case LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
