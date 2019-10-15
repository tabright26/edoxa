import { LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL, TransactionsActionTypes } from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: TransactionsActionTypes) => {
  switch (action.type) {
    case LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return state.filter(oldTransaction => !data.some(newTransaction => newTransaction.id === oldTransaction.id)).concat(data);
      }
    }
    case LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL: {
      return state;
    }
    default: {
      return state;
    }
  }
};
