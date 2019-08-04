import { LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS } from '../actions/userAccountActions';

export const reducer = (state = [], action) => {
  switch (action.type) {
    case LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS:
      return action.transactions;
    default:
      return state;
  }
};
