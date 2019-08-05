import { combineReducers } from 'redux';

import { reducer as userAccountBalanceReducer } from './userAccountBalanceReducer';
import { reducer as userAccountTransactionsReducer } from './userAccountTransactionsReducer';
import { reducer as userAccountStripeReducer } from './userAccountStripeReducer';

export const reducer = combineReducers({
  balance: userAccountBalanceReducer,
  transactions: userAccountTransactionsReducer,
  stripe: userAccountStripeReducer
});
