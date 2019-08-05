import { combineReducers } from 'redux';

import { reducer as userAccountReducer } from './userAccountReducer';
import { reducer as userGamesReducer } from './userGamesReducer';

import { persistReducer } from 'redux-persist';
import storage from 'redux-persist/lib/storage/session';

const persistConfig = {
  key: 'user',
  storage
};

export const reducer = persistReducer(
  persistConfig,
  combineReducers({
    account: userAccountReducer,
    games: userGamesReducer
  })
);
