import { HAS_BANKACCOUNT_SUCCESS } from '../actions/userAccountActions';

export const reducer = (state = false, action) => {
  switch (action.type) {
    case HAS_BANKACCOUNT_SUCCESS:
      return action.hasBankAccount;
    default:
      return state;
  }
};
