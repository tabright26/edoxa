import { combineReducers } from "redux";

import { reducer as balanceReducer } from "./balance/reducer";
import { reducer as depositReducer } from "./deposit/reducer";
import { reducer as withdrawalReducer } from "./withdrawal/reducer";

export const reducer = combineReducers({
  balance: balanceReducer,
  deposit: depositReducer,
  withdrawal: withdrawalReducer
});
