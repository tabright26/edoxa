import { combineReducers } from 'redux';

import { reducer as userAccountBalanceMoneyReducer } from './userAccountBalanceMoneyReducer';
import { reducer as userAccountBalanceTokenReducer } from './userAccountBalanceTokenReducer';

export const reducer = combineReducers({
  money: userAccountBalanceMoneyReducer,
  token: userAccountBalanceTokenReducer
});
