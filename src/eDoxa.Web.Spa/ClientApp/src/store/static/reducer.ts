import { combineReducers } from "redux";

import { reducer as transactionBundleReducer } from "./transactionBundle/reducer";

export const reducer = combineReducers({
    transactionBundle: transactionBundleReducer
});
