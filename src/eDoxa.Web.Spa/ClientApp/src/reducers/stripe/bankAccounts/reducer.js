import actionTypes from "actions/stripe";

export const initialState = { data: [] };

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case actionTypes.LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case actionTypes.LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL:
    default:
      return state;
  }
};
