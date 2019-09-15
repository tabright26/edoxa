import { combineReducers } from "redux";

import { reducer as challenges } from "./challenges/reducer";
import { reducer as games } from "./games/reducer";

export const reducer = combineReducers({
  challenges,
  games
});
