import { combineReducers } from "redux";

import { reducer as identityReducer } from "./identity/reducer";
import { reducer as transactionBundleReducer } from "./transactionBundle/reducer";

export const reducer = combineReducers({
  identity: identityReducer,
  transactionBundle: transactionBundleReducer
});
