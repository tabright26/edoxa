import { combineReducers } from "redux";

import { reducer as balanceReducer } from "./balance/reducer";
import { reducer as transactionsReducer } from "./transactions/reducer";

export const reducer = combineReducers({
  balance: balanceReducer,
  transactions: transactionsReducer
});
