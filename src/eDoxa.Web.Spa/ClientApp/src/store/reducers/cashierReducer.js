import { combineReducers } from 'redux';
import { reducer as userAccountTransactionsReducer } from './userAccountTransactionsReducer';
import { reducer as bankAccountReducer } from './userAccountStripeBankAccountsReducer';
import { reducer as cardsReducer } from './userAccountStripeCardsReducer';
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
    transactions: userAccountTransactionsReducer,
    cards: cardsReducer
  })
);
