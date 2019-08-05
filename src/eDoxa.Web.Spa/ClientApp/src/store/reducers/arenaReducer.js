import { combineReducers } from 'redux';

import { reducer as arenaChallengesReducer } from './arenaChallengesReducer';
import { reducer as arenaGamesReducer } from './arenaGamesReducer';

export const reducer = combineReducers({
  challenges: arenaChallengesReducer,
  games: arenaGamesReducer
});
