import actionTypes from "actions/cashier";

export const initialState = { type: "Token", available: 0, pending: 0 };

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case actionTypes.LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS:
      const { currency, available, pending } = action.payload.data;
      return {
        type: currency,
        available,
        pending
      };
    case actionTypes.LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL:
    default:
      return state;
  }
};
