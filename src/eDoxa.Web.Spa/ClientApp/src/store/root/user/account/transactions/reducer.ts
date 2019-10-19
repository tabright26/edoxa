import { LOAD_USER_ACCOUNT_TRANSACTIONS, LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL, UserAccountTransactionsActions, UserAccountTransactionsState } from "./types";
import { Reducer } from "redux";

export const initialState: UserAccountTransactionsState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<UserAccountTransactionsState, UserAccountTransactionsActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_ACCOUNT_TRANSACTIONS: {
      return {
        data: state.data,
        error: null,
        loading: true
      };
    }
    case LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204: {
          return {
            data: state.data,
            error: null,
            loading: false
          };
        }
        default: {
          return {
            data: data,
            error: null,
            loading: false
          };
        }
      }
    }
    case LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL: {
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
