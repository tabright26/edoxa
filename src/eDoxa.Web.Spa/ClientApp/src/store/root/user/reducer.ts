import { combineReducers } from "redux";

import { reducer as informationReducer } from "./information/reducer";
import { reducer as doxatagHistoryReducer } from "./doxatagHistory/reducer";
import { reducer as addressBookReducer } from "./addressBook/reducer";
import { reducer as accountReducer } from "./account/reducer";
import { reducer as phoneReducer } from "./phone/reducer";
import { reducer as emailReducer } from "./email/reducer";

export const reducer = combineReducers({
  email: emailReducer,
  phone: phoneReducer,
  account: accountReducer,
  information: informationReducer,
  doxatagHistory: doxatagHistoryReducer,
  addressBook: addressBookReducer
});
