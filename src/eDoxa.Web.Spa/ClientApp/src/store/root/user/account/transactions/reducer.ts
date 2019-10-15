import { LOAD_USER_ACCOUNT_TRANSACTIONS, LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL, TransactionsActionTypes, TransactionsState } from "./types";
import { Reducer } from "redux";

export const initialState: TransactionsState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<TransactionsState, TransactionsActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_ACCOUNT_TRANSACTIONS: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204: {
          return { data: state.data, error: null, loading: false };
        }
        default: {
          return { data: state.data.filter(oldTransaction => !data.some(newTransaction => newTransaction.id === oldTransaction.id)).concat(data), error: null, loading: false };
        }
      }
    }
    case LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
