import { HAS_BANKACCOUNT_SUCCESS } from '../actions/cashierActions';

export const reducer = (state = false, action) => {
  switch (action.type) {
    case HAS_BANKACCOUNT_SUCCESS:
      return action.hasBankAccount;
    default:
      return state;
  }
};
