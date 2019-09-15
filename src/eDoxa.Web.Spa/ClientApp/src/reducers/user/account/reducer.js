import { combineReducers } from "redux";

import { reducer as balance } from "./balance/reducer";
import { reducer as transactions } from "./transactions/reducer";

export const reducer = combineReducers({
  balance,
  transactions
});
