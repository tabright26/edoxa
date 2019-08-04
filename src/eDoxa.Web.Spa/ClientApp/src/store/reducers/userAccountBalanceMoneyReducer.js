import { LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS } from '../actions/userAccountActions';

const initialState = { type: 'Money', available: 0, pending: 0 };

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS:
      const { currency, available, pending } = action.token;
      return {
        type: currency,
        available,
        pending
      };
    default:
      return state;
  }
};
