import { LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS, LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL } from "../actions/stripeActions";

export const initialState = { data: [] };

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL:
    default:
      return state;
  }
};
