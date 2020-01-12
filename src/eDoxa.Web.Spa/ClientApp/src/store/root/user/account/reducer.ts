import { combineReducers } from "redux";

import { reducer as balanceReducer } from "./balance/reducer";

export const reducer = combineReducers({
  balance: balanceReducer
});
