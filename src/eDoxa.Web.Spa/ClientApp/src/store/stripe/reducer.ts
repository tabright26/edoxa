import { combineReducers } from "redux";

import { reducer as bankAccounts } from "./bankAccounts/reducer";
import { reducer as paymentMethods } from "./paymentMethods/reducer";

export const reducer = combineReducers({
  bankAccounts,
  paymentMethods
});
