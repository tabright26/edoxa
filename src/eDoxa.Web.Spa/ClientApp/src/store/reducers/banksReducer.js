import { HAS_USER_STRIPE_BANK_ACCOUNT_SUCCESS } from '../actions/userAccountActions';

export const reducer = (state = false, action) => {
  switch (action.type) {
    case HAS_USER_STRIPE_BANK_ACCOUNT_SUCCESS:
      return action.hasBankAccount;
    default:
      return state;
  }
};
