import { combineReducers } from "redux";

import { reducer as bundlesReducer } from "./bundles/reducer";

export const reducer = combineReducers({
  bundles: bundlesReducer
});
