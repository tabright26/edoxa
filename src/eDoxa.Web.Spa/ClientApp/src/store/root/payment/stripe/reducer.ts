import { combineReducers } from "redux";

import { reducer as paymentMethodsReducer } from "./paymentMethod/reducer";

export const reducer = combineReducers({
  paymentMethods: paymentMethodsReducer
});
