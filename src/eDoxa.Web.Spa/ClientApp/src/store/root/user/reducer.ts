import { combineReducers } from "redux";

import { reducer as personalInfoReducer } from "./personalInfo/reducer";
import { reducer as doxaTagHistoryReducer } from "./doxaTagHistory/reducer";
import { reducer as addressBookReducer } from "./addressBook/reducer";
import { reducer as accountReducer } from "./account/reducer";
import { reducer as gamesReducer } from "./games/reducer";
import { reducer as phoneNumberReducer } from "./phoneNumber/reducer";
import { reducer as emailReducer } from "./email/reducer";

export const reducer = combineReducers({
  email: emailReducer,
  phoneNumber: phoneNumberReducer,
  account: accountReducer,
  personalInfo: personalInfoReducer,
  doxaTagHistory: doxaTagHistoryReducer,
  addressBook: addressBookReducer,
  games: gamesReducer
});
