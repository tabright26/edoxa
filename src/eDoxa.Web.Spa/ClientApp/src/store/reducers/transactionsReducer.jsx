import { FETCH_TRANSACTIONS_SUCCESS } from '../actions/cashierActions';

export const reducer = (state = [], action) => {
  switch (action.type) {
    case FETCH_TRANSACTIONS_SUCCESS:
      return action.transactions;
    default:
      return state;
  }
};
