import {
  LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS,
  LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL
} from '../actions/userAccountActions';

export const reducer = (state = false, action) => {
  switch (action.type) {
    case LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return false;
        default:
          return data.data.length >= 1;
      }
    case LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL:
      console.log(action.payload.error);
      return state;
    default:
      return state;
  }
};
