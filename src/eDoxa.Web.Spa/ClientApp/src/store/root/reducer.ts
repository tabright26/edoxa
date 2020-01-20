import { combineReducers } from "redux";

import { reducer as paymentReducer } from "store/root/payment/reducer";
import { reducer as userReducer } from "store/root/user/reducer";
import { reducer as organizationReducer } from "store/root/organization/reducer";
import { reducer as challengeReducer } from "store/root/challenge/reducer";

export const reducer = combineReducers({
  payment: paymentReducer,
  user: userReducer,
  organization: organizationReducer,
  challenge: challengeReducer
});
