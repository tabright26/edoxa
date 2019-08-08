import { combineReducers } from 'redux';

import { reducer as userAccountReducer } from './userAccountReducer';
import { reducer as userGamesReducer } from './userGamesReducer';

export const reducer = combineReducers({
  account: userAccountReducer,
  games: userGamesReducer
});
