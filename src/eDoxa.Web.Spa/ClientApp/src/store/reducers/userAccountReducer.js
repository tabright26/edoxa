import { combineReducers } from 'redux';

import { reducer as userAccountBalanceReducer } from './userAccountBalanceReducer';

export const reducer = combineReducers({
  balance: userAccountBalanceReducer
});
