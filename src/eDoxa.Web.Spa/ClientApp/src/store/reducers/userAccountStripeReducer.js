import { combineReducers } from 'redux';

import { reducer as userAccountStripeBankAccountsReducer } from './userAccountStripeBankAccountsReducer';
import { reducer as userAccountStripeCardsReducer } from './userAccountStripeCardsReducer';

export const reducer = combineReducers({
  bankAccounts: userAccountStripeBankAccountsReducer,
  cards: userAccountStripeCardsReducer
});
