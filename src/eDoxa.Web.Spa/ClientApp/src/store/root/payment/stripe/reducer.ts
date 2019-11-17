import { combineReducers } from "redux";

import { reducer as accountReducer } from "./account/reducer";
import { reducer as customerReducer } from "./customer/reducer";
import { reducer as bankAccountReducer } from "./bankAccount/reducer";
import { reducer as paymentMethodsReducer } from "./paymentMethod/reducer";

export const reducer = combineReducers({
  account: accountReducer,
  customer: customerReducer,
  bankAccount: bankAccountReducer,
  paymentMethods: paymentMethodsReducer
});
