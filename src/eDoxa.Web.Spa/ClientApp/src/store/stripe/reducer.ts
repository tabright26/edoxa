import { combineReducers } from "redux";

import { reducer as bankAccounts } from "./bankAccounts/reducer";
import { reducer as cards } from "./cards/reducer";

export const reducer = combineReducers({
  bankAccounts,
  cards
});
