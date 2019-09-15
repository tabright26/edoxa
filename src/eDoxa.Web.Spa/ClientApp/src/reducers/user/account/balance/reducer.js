import { combineReducers } from "redux";

import { reducer as money } from "./money/reducer";
import { reducer as token } from "./token/reducer";

export const reducer = combineReducers({
  money,
  token
});
