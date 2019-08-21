import { combineReducers } from "redux";

import { reducer as userPersonalInfoReducer } from "./userPersonalInfoReducer";
import { reducer as userDoxaTagHistoryReducer } from "./userDoxaTagHistoryReducer";
import { reducer as userAddressBookReducer } from "./userAddressBookReducer";
import { reducer as userAccountReducer } from "./userAccountReducer";
import { reducer as userGamesReducer } from "./userGamesReducer";

export const reducer = combineReducers({
  account: userAccountReducer,
  personalInfo: userPersonalInfoReducer,
  doxaTagHistory: userDoxaTagHistoryReducer,
  addressBook: userAddressBookReducer,
  games: userGamesReducer
});
