import { LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS, LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL } from "../../../../../actions/cashierActions";

export const initialState = { type: "Money", available: 0, pending: 0 };

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS:
      const { currency, available, pending } = action.payload.data;
      return { type: currency, available, pending };
    case LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL:
    default:
      return state;
  }
};
