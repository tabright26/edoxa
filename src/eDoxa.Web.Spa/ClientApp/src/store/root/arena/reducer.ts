import { combineReducers } from "redux";

import { reducer as challenges } from "./challenges/reducer";

export const reducer = combineReducers({
  challenges
});
