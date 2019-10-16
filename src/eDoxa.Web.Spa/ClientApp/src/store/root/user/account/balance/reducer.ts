import { LOAD_USER_ACCOUNT_BALANCE, LOAD_USER_ACCOUNT_BALANCE_SUCCESS, LOAD_USER_ACCOUNT_BALANCE_FAIL, BalanceActions, BalanceState } from "./types";
import { Reducer } from "redux";

export const initialState: BalanceState = {
  data: { money: { available: 0, pending: 0 }, token: { available: 0, pending: 0 } },
  error: null,
  loading: false
};

export const reducer: Reducer<BalanceState, BalanceActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_ACCOUNT_BALANCE: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_USER_ACCOUNT_BALANCE_SUCCESS: {
      const { currency, available, pending } = action.payload.data;
      state.data[currency.toLowerCase()] = { available, pending };
      return { data: state.data, error: null, loading: false };
    }
    case LOAD_USER_ACCOUNT_BALANCE_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
