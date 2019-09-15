import actions from "../../../actions/stripe";

export const initialState = { data: [] };

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case actions.LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case actions.LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL:
    default:
      return state;
  }
};
