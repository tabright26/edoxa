import { LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS, LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL } from "../../../../../actions/cashierActions";

export const initialState = { type: "Token", available: 0, pending: 0 };

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS:
      const { currency, available, pending } = action.payload.data;
      return {
        type: currency,
        available,
        pending
      };
    case LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL:
    default:
      return state;
  }
};
