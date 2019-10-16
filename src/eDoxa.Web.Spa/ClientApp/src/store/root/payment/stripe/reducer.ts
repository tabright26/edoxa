import { combineReducers } from "redux";

import { reducer as accountReducer } from "./account/reducer";
import { reducer as bankAccountReducer } from "./bankAccount/reducer";
import { reducer as paymentMethodsReducer } from "./paymentMethods/reducer";

export const reducer = combineReducers({
  account: accountReducer,
  bankAccount: bankAccountReducer,
  paymentMethods: paymentMethodsReducer
});
