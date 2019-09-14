import { combineReducers } from "redux";

import { reducer as leagueOfLegendsReducer } from "./leagueOfLegends/reducer";

export const reducer = combineReducers({
  leagueOfLegends: leagueOfLegendsReducer
});
