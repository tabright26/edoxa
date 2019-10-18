import {
  LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES,
  LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS,
  LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL,
  UserAccountDepositBundlesState,
  UserAccountDepositBundlesActions
} from "../types";
import { Reducer } from "redux";

export const initialState: UserAccountDepositBundlesState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<UserAccountDepositBundlesState, UserAccountDepositBundlesActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES: {
      return {
        data: state.data,
        error: null,
        loading: true
      };
    }
    case LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS: {
      return {
        data: state.data,
        error: null,
        loading: false
      };
    }
    case LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL: {
      return {
        data: state.data,
        error: action.error,
        loading: false
      };
    }
    default: {
      return state;
    }
  }
};
