import { combineReducers } from "redux";

import { reducer as challengesReducer } from "./challenges/reducer";

export const reducer = combineReducers({
  challenges: challengesReducer
});
