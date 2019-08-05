import { combineReducers } from 'redux';

import { reducer as arenaGamesLeagueOfLegendsReducer } from './arenaGamesLeagueOfLegendsReducer';

export const reducer = combineReducers({
  leagueOfLegends: arenaGamesLeagueOfLegendsReducer
});
