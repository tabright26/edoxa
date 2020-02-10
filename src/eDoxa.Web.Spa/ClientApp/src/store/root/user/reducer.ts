import { combineReducers } from "redux";

import { reducer as profileReducer } from "./profile/reducer";
import { reducer as doxatagHistoryReducer } from "./doxatagHistory/reducer";
import { reducer as addressBookReducer } from "./addressBook/reducer";
import { reducer as accountReducer } from "./account/reducer";
import { reducer as phoneReducer } from "./phone/reducer";
import { reducer as emailReducer } from "./email/reducer";
import { reducer as transactionHistoryReducer } from "./transactionHistory/reducer";

export const reducer = combineReducers({
  email: emailReducer,
  phone: phoneReducer,
  account: accountReducer,
  profile: profileReducer,
  doxatagHistory: doxatagHistoryReducer,
  addressBook: addressBookReducer,
  transactionHistory: transactionHistoryReducer
});
