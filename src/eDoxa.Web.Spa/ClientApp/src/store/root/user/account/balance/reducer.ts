import { combineReducers } from "redux";

import { reducer as moneyReducer } from "./money/reducer";
import { reducer as tokenReducer } from "./token/reducer";

export const reducer = combineReducers({
  money: moneyReducer,
  token: tokenReducer
});
