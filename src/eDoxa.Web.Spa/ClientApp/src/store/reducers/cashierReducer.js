import { combineReducers } from 'redux';
import { reducer as transactionsReducer } from './transactionsReducer';
import { reducer as bankAccountReducer } from './banksReducer';
import { reducer as cardsReducer } from './cardsReducer';
import { reducer as accountReducer } from './userAccountBalanceReducer';

import { persistReducer } from 'redux-persist';
import storage from 'redux-persist/lib/storage/session'; // defaults to localStorage for web

const persistConfig = {
  key: 'cashier',
  storage
};

export const reducer = persistReducer(
  persistConfig,
  combineReducers({
    account: accountReducer,
    hasBankAccount: bankAccountReducer,
    transactions: transactionsReducer,
    cards: cardsReducer
  })
);
