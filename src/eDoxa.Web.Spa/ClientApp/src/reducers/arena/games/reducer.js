import { combineReducers } from "redux";

import { reducer as leagueOfLegends } from "./leagueOfLegends/reducer";

export const reducer = combineReducers({
  leagueOfLegends
});
