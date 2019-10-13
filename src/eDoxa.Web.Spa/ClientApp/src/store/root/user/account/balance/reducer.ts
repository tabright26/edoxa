import { LOAD_USER_ACCOUNT_BALANCE_SUCCESS, LOAD_USER_ACCOUNT_BALANCE_FAIL, BalanceActionTypes } from "./types";

export const initialState = { money: { available: 0, pending: 0 }, token: { available: 0, pending: 0 } };

export const reducer = (state = initialState, action: BalanceActionTypes) => {
  switch (action.type) {
    case LOAD_USER_ACCOUNT_BALANCE_SUCCESS:
      const { currency, available, pending } = action.payload.data;
      state[currency.toLowerCase()] = { available, pending };
      return state;
    case LOAD_USER_ACCOUNT_BALANCE_FAIL:
    default:
      return state;
  }
};
