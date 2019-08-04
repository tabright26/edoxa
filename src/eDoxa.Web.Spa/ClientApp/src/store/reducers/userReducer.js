import { combineReducers } from 'redux';

import { reducer as userAccountReducer } from './userAccountReducer';
//import { reducer as gameAccountReducer } from "./gameAccountReducer";

export const reducer = combineReducers({
  account: userAccountReducer
  // gameAccounts: gameAccountReducer
});
