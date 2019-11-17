import { combineReducers } from "redux";

import { reducer as moneyReducer } from "./money/reducer";

export const reducer = combineReducers({
  money: moneyReducer
});
