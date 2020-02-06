import { combineReducers } from "redux";

import { reducer as customerReducer } from "./customer/reducer";
import { reducer as bankAccountReducer } from "./bankAccount/reducer";
import { reducer as paymentMethodsReducer } from "./paymentMethod/reducer";

export const reducer = combineReducers({
  customer: customerReducer,
  bankAccount: bankAccountReducer,
  paymentMethods: paymentMethodsReducer
});
