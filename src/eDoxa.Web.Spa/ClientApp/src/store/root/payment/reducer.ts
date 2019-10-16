import { combineReducers } from "redux";

import { reducer as stripeReducer } from "./stripe/reducer";

export const reducer = combineReducers({
  stripe: stripeReducer
});
