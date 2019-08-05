import { combineReducers } from 'redux';

import { reducer as arenaChallengesReducer } from './arenaChallengesReducer';
import { reducer as arenaGamesReducer } from './arenaGamesReducer';

import { persistReducer } from 'redux-persist';
import storage from 'redux-persist/lib/storage/session';

const persistConfig = {
  key: 'arena',
  storage,
  blacklist: ['challenges']
};

export const reducer = persistReducer(
  persistConfig,
  combineReducers({
    challenges: arenaChallengesReducer,
    games: arenaGamesReducer
  })
);
